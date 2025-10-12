using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using WebUI.Models;
using WebUI.Models.DataTableRequest;
using WebUI.Models.ViewModels;
using Persistence.Utils;
using System.IO;
using System.Net.Mime;
using IronBarCode;
using System.Text;
using WebUI.Models.ApiRequest;
using System.Drawing;

namespace WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ISystemSession _systemSession;
        private readonly IRouteService _routeService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IBatchService _batchService;
        private readonly EplusDbContext _context;
        private readonly ICouponService _couponService;
        private readonly ISystemSession _session;

        public OrderController(IOrderService orderService, ISystemSession systemSession, ILogger<OrderController> logger, IMapper mapper, IRouteService routeService, IUserService userService, IBatchService batchService, EplusDbContext context, ICouponService couponService, ISystemSession session)
        {
            _orderService = orderService;
            _systemSession = systemSession;
            _logger = logger;
            _mapper = mapper;
            _routeService = routeService;
            _userService = userService;
            _batchService = batchService;
            _context = context;
            _couponService = couponService;
            _session = session;
        }

        public IActionResult Inventory(OrderState orderState)
        {
            var result = new OrderInventoryResponse()
            {
                OrderState = orderState
            };
            return View(result);
        }
        public IActionResult InWarehouseInventory()
        {
            var result = new OrderInventoryResponse()
            {
                OrderState = OrderState.InWarehouse
            };
            return View(result);
        }
        public IActionResult ContrabandInventory()
        {
            var result = new OrderInventoryResponse()
            {
                OrderState = OrderState.Illegal
            };
            return View(result);
        }

        public IActionResult Search()
        {
            return View();
        }

        public async Task<IActionResult> LoadData(OrderState? orderState, DataTableRequestModel requestModel)
        {
            var orderNumberToSearch = requestModel.GetColumnSearchValue("OrderNumber");
            var domesticNumberToSearch = requestModel.GetColumnSearchValue("DomesticNumber");
            var creatorToSearch = requestModel.GetColumnSearchValue("Creator");

            var orders = await _orderService.ListAsync(new OrderListFilterOptions()
            {
                OrderNumberToSearch = orderNumberToSearch,
                DomesticNumberToSearch = domesticNumberToSearch,
                OrderState = orderState,
                CreatorToSearch = creatorToSearch,
                PageSize = requestModel.Length,
                Skip = requestModel.Start
            });

            var data = new PagedResult<OrderInventoryViewModel>()
            {
                Total = orders.Total,
                Items = orders.Items.Select(it => _mapper.Map<OrderInventoryViewModel>(it))
            };

            return Json(new { draw = requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = data.Items});
        }

        public async Task<IActionResult> SetOrderState(int id, OrderState state, string reason)
        {
            await _orderService.SetOrderState(id, state, reason);
            return Json(new MethodResult<bool>(true));
        }

        public async Task<IActionResult> QuickView(int id)
        {
            var orderEntity = await _orderService.GetAsync(id);
            var order = _mapper.Map<OrderDetailViewModel>(orderEntity);
            return ViewComponent("OrderInfo", new {order});
        }

        public async Task<IActionResult> Delete(int id, OrderState orderState)
        {
            await _orderService.Delete(id);
            return RedirectToAction(nameof(Inventory), new {orderState});
        }

        public async Task<IActionResult> Edit(int id)
        {
            var orderEntity = await _orderService.GetAsync(id);
            var model = _mapper.Map<OrderDetailEditModel>(orderEntity);
            var scanStatusList = _batchService.GetOrderScanStatusEntities(new [] { id });
            model.Operations = model.Status.Select(s => new OrderOperationModel
            {
                Action = s.GetDisplayText(),
                Operator = s.Operator.Name,
                Timestamp = s.Date
            }).Union(scanStatusList.Where(s => s.Status != OrderScanStatusType.Unknown).Select(s => new OrderOperationModel
            {
                Action = s.GetDisplayText(),
                Operator = s.UserName,
                Timestamp = s.Timestamp
            })).Union(model.InternalStatus.Select(s => new OrderOperationModel
            {
                Action = s.GetDisplayText(),
                Operator = s.Operator.Name,
                Timestamp = s.Date
            }));

            if (model.BaggageEditModels.Count == 0)
            {
                model.BaggageEditModels.Add(new OrderBaggageEditModel() { Action = ActionType.Add });
            }
            
            model.Routes = await _routeService.ListAsync();
            model.ItemCategories = RouteEntity.Items;
            
            var batches = await _batchService.GetByOrderAsync(id);
            foreach (var batchEntity in batches)
            {
                var box = batchEntity.Boxes[0];
                model.BatchBoxes.Add(new OrderBatchBoxModel() { BatchId = batchEntity.Id, BoxId = box.Id, BatchName = batchEntity.Name, BoxNumber = box.Number });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> UploadPhoto(int orderId, string photoData)
        {
            try
            {
                var entity = await _orderService.AddPhotoAsync(orderId, photoData);
                _orderService.ClearCache(orderId);
                return Json(new MethodResult<OrderPhotoEntity>(entity));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<OrderPhotoEntity>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeletePhoto(int photoId, int orderId)
        {
            await _orderService.DeletePhotoAsync(photoId);
            _orderService.ClearCache(orderId);
            return Json(new MethodResult<bool>(true));
        }

        public async Task<IActionResult> Create(OrderState orderState)
        {
            var routes = await _routeService.ListAsync();
            var users = await _userService.ListAsync(new UserListFilterOptions()
            {
                PageSize = int.MaxValue
            });
            var locations = await _userService.ListPickUpLocationsAsync();
            return View("Draft", new OrderDraftViewModel() {
                Users = users.Items, Routes = routes,
                OrderState = orderState,
                PickupLocations = locations
            });
        }

        public async Task<IActionResult> BatchCreateDiscount()
        {
            ViewBag.Routes = await _routeService.ListAsync();
            return View("BatchCreateDiscount");
        }

        [HttpPost]
        public async Task<IActionResult> BatchCreateDiscountOrder(string prefix, string start, string end, int routeId, string batchName, string validFrom, string validUntil, decimal shippingCost, decimal minimumPrice = 0)
        {
            try
            {
                var batchId = await _batchService.CreateCouponBatchAsync(batchName);
                await _orderService.BatchCreateOrderAsync(prefix, start, end, routeId, batchId, validFrom, validUntil, shippingCost, minimumPrice);
                
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("inner exception") && e.InnerException != null)
                {
                    return Json(new MethodResult<bool>(new Error() { Text = e.InnerException.Message }));
                }
                else
                {
                    return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
                }
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(OrderDetailEditModel model)
        {
            // TODO: validate model => Item has mandatory field filled out
            var entity = _mapper.Map<OrderEntity>(model);
            entity.Items = model.ItemEditModels.Where(m =>
                (m.Id > 0 && m.Action == ActionType.Keep) || (m.Id == 0 && m.Action == ActionType.Add));
            entity.Baggages = model.BaggageEditModels.Where(m =>
                (m.Id > 0 && m.Action == ActionType.Keep) || (m.Id == 0 && m.Action == ActionType.Add));

            entity.Creator = await _userService.GetAsync(model.Creator.Id);
            var result = await _orderService.SaveAsync(entity);
            return RedirectToAction(nameof(Edit), new {id = result.Id});
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveDraft(OrderDraftViewModel model)
        {
            var entity = new OrderEntity()
            {
                DomesticNumber = model.DomesticNumber, DomesticCarrier = model.DomesticCarrier,
                Creator = await _userService.GetAsync(model.RecipientId),
                DraftById = _systemSession.CurrentUser.Id,
                RouteId = model.RouteId,
                State = model.OrderState,
                PickUpLocationId = model.PickupLocationId
            };
            var result = await _orderService.SaveDraftAsync(entity);
            return RedirectToAction(nameof(Edit), new { id = result.Id });
        }

        [HttpPost]
        public async Task<IActionResult> SetCouponUser([FromBody] SetCouponUserRequest request)
        {
            try
            {
                var coupon = await _context.Coupons.Include(c => c.CouponBatch).FirstOrDefaultAsync(c => c.Id == request.CouponId);
                if (string.IsNullOrWhiteSpace(coupon.CouponBatch.PhotoUrl))
                {
                    throw new Exception("请先上传优惠券图片");
                }
                if (coupon.AssignedUserId != null)
                {
                    throw new Exception("该优惠券已被指定用户");
                }
                if (!request.UserId.HasValue)
                {
                    throw new Exception("请指定用户");
                }
                var user = await _userService.GetAsync(request.UserId.Value);
                if (user == null)
                {
                    throw new Exception($"该用户{request.UserId}不存在");
                }
                if (string.IsNullOrWhiteSpace(user.Mailbox))
                {
                    throw new Exception($"该用户{request.UserId}没有邮箱");
                }
                coupon.AssignedUserId = request.UserId;
                await _context.SaveChangesAsync();
                // To get app password:
                // Gmail -> Settings -> See all settings -> Accounts and Import -> Other Google Account settings -> Security
                // Under Signing in to Google -> App passwords
                // To generate a new app password, select app: Mail, device: Windows Computer
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("notification.eplus@gmail.com", "dybqcagazakncdqb"),
                    EnableSsl = true
                };
                string[] paragraphs = request.Content.Split("\n");
                var mail = new MailMessage();
                mail.From = new MailAddress("notification.eplus@gmail.com");
                mail.To.Add(user.Mailbox);
                mail.Subject = MessageUtils.SetCouponUserEmailSubject;
                mail.AlternateViews.Add(GetEmbeddedImage(coupon.CouponBatch.PhotoUrl, coupon.DomesticNumber, paragraphs));
                mail.IsBodyHtml = true;
                smtpClient.Send(mail);
                _couponService.AddStatus(CouponStatusType.CouponAssigned, _session.CurrentUser.Id, new int[] { request.CouponId });
                await _context.SaveChangesAsync();

                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }

        private AlternateView GetEmbeddedImage(string photoUrl, string domesticNumber, string[] paragraphs)
        {
            var webClient = new WebClient();

            //byte[] barCodeImageBytes = BarcodeWriter.CreateBarcode(domesticNumber, BarcodeEncoding.Code128, 600, 75).AddBarcodeValueTextBelowBarcode().ToJpegBinaryData();
            var b = new BarcodeLib.Barcode()
            {
                IncludeLabel = true
            };
            var image = b.Encode(BarcodeLib.TYPE.CODE128, domesticNumber, System.Drawing.Color.Black, System.Drawing.Color.White, 600, 75);
            var couponImageBytes = webClient.DownloadData(photoUrl);
            var ic = new ImageConverter();
            byte[] byteArray = (byte[]) ic.ConvertTo(image, typeof(byte[]));
            MemoryStream barCodeMs = new MemoryStream(byteArray);
            //image.Save(barCodeMs, System.Drawing.Imaging.ImageFormat.Bmp);
            MemoryStream couponMs = new MemoryStream(couponImageBytes);
            LinkedResource barCodeRes = new LinkedResource(barCodeMs);
            barCodeRes.ContentId = Guid.NewGuid().ToString();
            LinkedResource couponRes = new LinkedResource(couponMs, MediaTypeNames.Image.Jpeg);
            couponRes.ContentId = Guid.NewGuid().ToString();
            StringBuilder builder = new StringBuilder();
            foreach (string p in paragraphs)
            {
                builder.Append($"<p>{p}</p>");
            }
            builder.Append($"<div style='text-align: center'><img style='width: 600px;' src='cid:{couponRes.ContentId}'/></div>");
            builder.Append("<br />");
            builder.Append($"<div style='text-align: center'><img style='width: 600px; height: 75px;' src='cid:{barCodeRes.ContentId}'/></div>");
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(builder.ToString(), null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(barCodeRes);
            alternateView.LinkedResources.Add(couponRes);
            return alternateView;
            
        }

        public async Task<JsonResult> CalculateItemCost(OrderDetailEditModel model)
        {
            var items = model.ItemEditModels.Where(m =>
                (m.Id > 0 && m.Action == ActionType.Keep) || (m.Id == 0 && m.Action == ActionType.Add)).ToList();
            
            if (items.Count == 0 || !model.RouteId.HasValue)
            {
                return Json(new MethodResult<decimal>(0));
            }

            var itemCost = await _orderService.CalculateItemCostAsync(new OrderEntity()
            {
                RouteId = model.RouteId,
                Items = items,
                WeightKg = model.WeightKg
            });
            
            return Json(new MethodResult<decimal>(itemCost));
        }

        public IActionResult AddItem(int index)
        {
            return PartialView("OrderItem", new OrderItemWithIndex()
            {
                Index = index,
                Item = new OrderItemEditModel() { Action = ActionType.Add },
                AllCategories = RouteEntity.Items
            });
        }

        public IActionResult AddBaggage(int index)
        {
            return PartialView("OrderBaggage", new OrderBaggageWithIndex()
            {
                Index = index,
                Baggage = new OrderBaggageEditModel() { Action = ActionType.Add }
            });
        }

        public async Task<IActionResult> PrintBarcode(int id)
        {
            var orderEntity = await _orderService.GetAsync(id);
            return View(orderEntity);
        }

        public async Task<IActionResult> PrintSmallBarcode(int id)
        {
            var orderEntity = await _orderService.GetAsync(id);
            return View(orderEntity);
        }

        public async Task<IActionResult> ReturnComplete(int id, OrderState orderState)
        {
            await _orderService.ReturnCompleteAsync(id);
            return RedirectToAction(nameof(Inventory), new {orderState});
        }
    }
}
