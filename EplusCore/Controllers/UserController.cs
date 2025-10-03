using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using ClosedXML.Extensions;
using Common;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Extensions;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Models.ApiRequest;
using WebUI.Models.DataTableRequest;
using WebUI.Models.ViewModels;
using Persistence.Utils;
using Domain.Enums;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly ISmsService _smsService;
        private readonly ISystemSession _session;
        private readonly IMapper _mapper;
        private readonly IFileExportService _fileExportService;
        private readonly EplusDbContext _context;

        public UserController(ITransactionService transactionService, ISystemSession session, IUserService userService, IMapper mapper, IFileExportService fileExportService, ISmsService smsService, EplusDbContext context)
        {
            _transactionService = transactionService;
            _session = session;
            _userService = userService;
            _mapper = mapper;
            _fileExportService = fileExportService;
            _smsService = smsService;
            _context = context;
        }

        public IActionResult Transactions(int? userId)
        {
            return View(userId ?? _session.CurrentUser.Id);
        }

        public async Task<IActionResult> LoadTransactionsByType(int userId, TransactionType type, DataTableRequestModel requestModel)
        {
            if (type == TransactionType.SelfDeposit)
            {
                var transactions = _context.BalanceHistories
                    .Where(h => h.ToUserId == userId && h.FromUserId == h.ToUserId)
                    .Include(b => b.FromUser).ThenInclude(u => u.Customer)
                    .Include(b => b.FromUser).ThenInclude(u => u.BelongsToNavigation).ThenInclude(u => u.Customer)
                    .Include(b => b.ToUser).ThenInclude(u => u.Customer)
                    .Include(b => b.ToUser).ThenInclude(u => u.BelongsToNavigation).ThenInclude(u => u.Customer)
                    .Include(b => b.Order)
                    .Include(b => b.Batch)
                    .OrderByDescending(b => b.Date);

                var total = await transactions.CountAsync();
                var pagedTransactions = transactions.Skip(requestModel.Start).Take(requestModel.Length + 25);
                var items = await pagedTransactions.Select(t => _mapper.Map<TransactionEntity>(t)).ToListAsync();
                var viewModels = items.Select(it =>
                {
                    var vm = _mapper.Map<TransactionViewModel>(it);
                    vm.User = it.FromUser.Id == userId ? it.ToUser : it.FromUser;
                    vm.CurrentBalance = it.ToUser.Id == userId ? it.ToUserCurrentBalance : it.FromUserCurrentBalance;
                    return vm;
                }).ToList();
                return Json(new { draw = requestModel.Draw, recordsFiltered = total, recordsTotal = total, data = viewModels });
            }
            else
            {
                throw new NotImplementedException("Unsupported Transaction Types");
            }
        }

        public async Task<IActionResult> LoadTransactions(int userId, DataTableRequestModel requestModel)
        {
            var data = await _transactionService.ListAsync(userId, new FilterOptions()
            {
                Skip = requestModel.Start,
                PageSize = requestModel.Length
            });
            var viewModels = data.Items.Select(it =>
            {
                var vm = _mapper.Map<TransactionViewModel>(it);
                vm.User = it.FromUser.Id == userId ? it.ToUser : it.FromUser;
                vm.CurrentBalance = it.ToUser.Id == userId ? it.ToUserCurrentBalance : it.FromUserCurrentBalance;
                return vm;
            }).ToList();
            viewModels[0].ColorIndex = 1;
            for (var i = 1; i < viewModels.Count; i++)
            {
                if (viewModels[i].Date == viewModels[i-1].Date)
                {
                    viewModels[i].ColorIndex = viewModels[i-1].ColorIndex;
                }
                else
                {
                    viewModels[i].ColorIndex = viewModels[i-1].ColorIndex + 1;
                }
            }

            return Json(new { draw = requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = viewModels });
        }

        public IActionResult Inventory()
        {
            return View();
        }

        public async Task<IActionResult> GetBalanceSummary()
        {
            try
            {
                var totalBalance = await _userService.GetBalanceSummaryAsync();
                return Json(new MethodResult<decimal>(totalBalance));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = nameof(GetBalanceSummary),
                    Text = e.Message
                }));
            }
        }

        public async Task<IActionResult> LoadUsers(DataTableRequestModel requestModel)
        {
            var codeToSearch = requestModel.GetColumnSearchValue("Code").Trim();
            var phoneToSearch = requestModel.GetColumnSearchValue("CanadaPhoneNumber").Trim();

            var users = await _userService.ListAsync(new UserListFilterOptions()
            {
                CodeToSearch = codeToSearch,
                PhoneToSearch = phoneToSearch,
                Skip = requestModel.Start,
                PageSize = requestModel.Length
            }, false);

            var data = new PagedResult<UserInventoryViewModel>()
            {
                Total = users.Total,
                Items = users.Items.Select(u => _mapper.Map<UserInventoryViewModel>(u))
            };

            return Json(new { draw = requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = data.Items });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetAsync(id);
            var agents = await _userService.ListAgentsAsync();
            var pickUpLocations = await _userService.ListPickUpLocationsAsync(2);
            var result = _mapper.Map<UserDetailViewModel>(user);
            result.RegisteredPickUpLocation ??= new PickUpLocationEntity();
            result.PickUpLocations = pickUpLocations.ToList();
            result.Agents = agents.ToList();
            return View(result);
        }

        public async Task<IActionResult> BalanceTransfer(int toUserId)
        {
            var fromUser = await _userService.GetAsync(_session.CurrentUser.Id);
            var toUser = await _userService.GetAsync(toUserId);
            return View(new BalanceTransferViewModel()
            {
                FromUser = _mapper.Map<UserDetailViewModel>(fromUser), 
                ToUser = _mapper.Map<UserDetailViewModel>(toUser),
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> SendCustomSMS(SendCustomSMSRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message)) return Json(new MethodResult<object>(new Error
            {
                Name = "NoMessage",
                Text = "Please enter some message."
            }));
            var smsUserInfo = await _smsService.GetSmsUserInfoByUserIdAsync(request.RecipientUserId);
            var userId = _session.CurrentUser.Id;
            await _smsService.SendAsync(new SmsRequest[]
            {
                new SmsRequest
                {
                    Message = request.Message,
                    MobilePhoneNumber = smsUserInfo.MobilePhoneNumber,
                    OrderStartNumber = smsUserInfo.OrderStartNumber,
                    BelongsTo = smsUserInfo.BelongsToName,
                    FullName = smsUserInfo.FullName,
                    Level = smsUserInfo.Level
                }
            }, userId);
            return Json(new MethodResult<bool>(true));
        }

        [HttpPost]
        public async Task<IActionResult> SendCustomEmail(SendCustomEmailRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Subject)) return Json(new MethodResult<object>(new Error
            {
                Name = "NoSubject",
                Text = "Please enter a subject."
            }));
            if (string.IsNullOrWhiteSpace(request.Message)) return Json(new MethodResult<object>(new Error
            {
                Name = "NoMessage",
                Text = "Please enter some message."
            }));
            var recipient = await _userService.GetAsync(request.RecipientUserId);
            if (string.IsNullOrWhiteSpace(recipient.Mailbox)) return Json(new MethodResult<object>(new Error
            {
                Name = "NoEmailAddress",
                Text = "This user has no email address."
            }));

            // To get app password:
            // Gmail -> Settings -> See all settings -> Accounts and Import -> Other Google Account settings -> Security
            // Under Signing in to Google -> App passwords
            // To generate a new app password, select app: Mail, device: Windows Computer
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(Constants.GmailAddress, Constants.GmailPwd),
                EnableSsl = true
            };
            smtpClient.Send(Constants.GmailAddress, recipient.Mailbox, request.Subject, request.Message);
            return Json(new MethodResult<bool>(true));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult BalanceTransfer(BalanceTransferViewModel model)
        {
            if (model.Info.Amount <= 0)
            {
                throw new ArgumentException("转的有点少吧");
            }

            model.Info.FromUserId = _session.CurrentUser.Id;
            model.Info.ToUserId = model.ToUser.Id;
            _userService.Deposit(model.Info);
            
            return RedirectToAction(nameof(BalanceTransfer), new { toUserId = model.ToUser.Id});
        }

        public async Task<IActionResult> ChangePassword(int id)
        {
            var user = await _userService.GetAsync(id);
            var account = new AccountViewModel() {Id = user.Id, UserCode = user.Code, UserName = user.UserName};
            return View(account);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(AccountViewModel account)
        {
            await _userService.ChangePassword(account.Id, account.Password);

            if (_session.CurrentUser.Id == account.Id)
            {
                return RedirectToAction("LogOff", "Account");
            }

            return RedirectToAction(nameof(Inventory));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(UserEntity user)
        {
            var result = await _userService.SaveAsync(user);
            return RedirectToAction(nameof(Edit), new {id = result.Id});
        }

        [HttpPost]
        public async Task<IActionResult> SetRouteVisibility(int userId, int routeId, bool isVisible)
        {
            await _userService.SetRouteVisibilityAsync(userId, routeId, isVisible);
            return Json(new MethodResult<bool>(true));
        }

        public async Task<IActionResult> Export()
        {
            var users = await _userService.ListAsync(new UserListFilterOptions()
            {
                PageSize = int.MaxValue
            });

            using var result = _fileExportService.Export(users.Items, "user");
            var wb = result as XLWorkbook;
            Response.Headers.Add("Set-Cookie", "fileDownload=true; path=/");
            return wb.Deliver("Users.xlsx");
        }

        public async Task<IActionResult> ExportBill(int userId)
        {
            try
            {
                var user = await _userService.GetAsync(userId);
                
                var transactions = await _transactionService.ListAsync(userId, new FilterOptions
                {
                    PageSize = 10000
                });
                using var result = _fileExportService.Export(transactions.Items, "bill", user);
                var wb = result as XLWorkbook;
                Response.Headers.Add("Set-Cookie", "fileDownload=true; path=/");
                return wb.Deliver($"Transactions_{user.OrderStartNumber}.xlsx");
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = "ExportBill",
                    Text = e.Message
                }));
            }
        }
    }
}
