using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using ClosedXML.Extensions;
using Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Persistence.Utils;
using Persistence.Data;
using WebUI.Models;
using WebUI.Models.DataTableRequest;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class BatchController : Controller
    {
        private readonly IBatchService _batchService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IWarehouseService _warehouseService;
        private readonly IRouteService _routeService;
        private readonly IFileExportService _fileExportService;
        private readonly INotificationService _notificationService;
        private readonly ISmsService _smsService;
        private readonly ISystemSession _session;
        private readonly IMapper _mapper;
        private readonly EplusDbContext _context;
        private readonly ICouponService _couponService;
        private readonly IEmailService _emailService;
        private readonly IServiceProvider _serviceProvider;

        public BatchController(
            IBatchService batchService,
            IMapper mapper,
            IOrderService orderService,
            IUserService userService,
            IWarehouseService warehouseService,
            IRouteService routeService,
            IFileExportService fileExportService,
            INotificationService notificationService,
            ISystemSession session,
            ISmsService smsService,
            EplusDbContext context,
            ICouponService couponService,
            IEmailService emailService,
            IServiceProvider serviceProvider)
        {
            _batchService = batchService;
            _mapper = mapper;
            _orderService = orderService;
            _smsService = smsService;
            _userService = userService;
            _warehouseService = warehouseService;
            _routeService = routeService;
            _fileExportService = fileExportService;
            _notificationService = notificationService;
            _session = session;
            _context = context;
            _couponService = couponService;
            _emailService = emailService;
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Inventory(BatchGroupType groupType, int? routeId, int? warehouseId, int? recipientUserId, int? belongsToUserId)
        {
            var warehouses = (await _warehouseService.ListAsync()).ToList();
            var routes = (await _routeService.ListAsync()).Where(r => !r.IsDeleted).ToList();
            var isDisplayByWarehouse = groupType == BatchGroupType.DailyScan || groupType == BatchGroupType.DailyReturn;
            var users = await _userService.ListByBatchesAsync(groupType, routeId, warehouseId);
            var result = new BatchInventoryResponse()
            {
                GroupType = groupType,
                SelectedRouteId = isDisplayByWarehouse ? (int?) null : (routeId ?? routes.First().Id),
                Routes = routes.OrderBy(r => r.DisplaySequence),
                SelectedWarehouseId = isDisplayByWarehouse ? (warehouseId ?? warehouses.First().Id) : (int?) null,
                Warehouses = warehouses.OrderBy(w => w.DisplaySequence),
                Users = users,
                SelectedRecipientUserId = recipientUserId,
                SelectedBelongsToUserId = belongsToUserId
            };
            if (groupType == BatchGroupType.DailyScan)
            {
                return View("DailyScanInventory", result);
            }
            if (groupType == BatchGroupType.LoadDelivery)
            {
                return View("LoadDeliveryInventory", result);
            }
            else
            {
                return View(result);
            }
        }

        public IActionResult Refresh(int id, int? boxId)
        {
            _batchService.RemoveCache(id);
            if (boxId.HasValue)
            {
                return RedirectToAction(nameof(EditBox), new {boxId});
            }
            else
            {
                return RedirectToAction(nameof(Edit), new { id });
            }
        }

        public IActionResult OtherOrder()
        {
            return View();
        }

        public IActionResult CouponInventory()
        {
            return View();
        }

        public async Task<IActionResult> CreateDailyBatchPerWarehouse(BatchGroupType groupType)
        {
            await _batchService.CreateDailyBatchPerWarehouseAsync(groupType);
            return RedirectToAction(nameof(Inventory), new {groupType});
        }

        public async Task<IActionResult> LoadOtherOrder(DataTableRequestModel requestModel)
        {
            var orderToSearch = requestModel.GetColumnSearchValue("OtherOrder");
            var data = await _batchService.ListOtherOrderAsync(new BatchListOtherOrderFilterOptions()
            {
                Number = orderToSearch,
                PageSize = requestModel.Length,
                Skip = requestModel.Start
            });

            return Json(new { requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = data.Items });
        }

        public async Task<IActionResult> LoadCouponBatch(DataTableRequestModel requestModel)
        {
            try
            {
                var options = new FilterOptions
                {
                    PageSize = requestModel.Length,
                    Skip = requestModel.Start
                };
                var data = await _batchService.ListCouponBatchAsync(options);
                return Json(new { requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = data.Items });
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = "LoadCouponBatch",
                    Text = e.Message
                }));
            }
        }

        public async Task<IActionResult> LoadData(BatchGroupType groupType, int? routeId, int? warehouseId, int? recipientUserId, int? belongsToUserId, DataTableRequestModel requestModel)
        {
            try
            {
                var filter = new BatchListFilterOptions()
                {
                    GroupType = groupType,
                    WarehouseId = warehouseId,
                    RouteId = routeId,
                    PageSize = requestModel.Length,
                    Skip = requestModel.Start
                };
                if (recipientUserId.HasValue)
                {
                    filter.RecipientIds.Add(recipientUserId.Value);
                }
                if (belongsToUserId.HasValue)
                {
                    filter.BelongsToUserIds.Add(belongsToUserId.Value);
                }
                var data = await _batchService.ListAsync(filter);

                var viewModels = data.Items.Select(it => _mapper.Map<BatchViewModel>(it)).ToList();
                return Json(new { requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = viewModels });
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = "LoadDataError",
                    Text = e.Message
                }));
            }
        }

        public async Task<IActionResult> QuickView(int id)
        {
            var batchEntity = await _batchService.GetAsync(id);
            var batch = _mapper.Map<BatchViewModel>(batchEntity);
            return ViewComponent("BatchInfo", new {batch});
        }

        public async Task<IActionResult> DailyScanEdit(int id)
        {
            var batchEntity = await _batchService.GetForEditAsync(id);
            await SetEditDropdownOptions(batchEntity);
            return View(batchEntity);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var batchEntity = await _batchService.GetForEditAsync(id);
            await SetEditDropdownOptions(batchEntity);
            return View(batchEntity);
        }

        public async Task<IActionResult> EditCoupon(int id)
        {
            try
            {
                var users = await _userService.ListAsync(new UserListFilterOptions()
                {
                    PageSize = int.MaxValue
                });
                ViewBag.Users = users.Items;
                var batchEntity = await _batchService.GetCouponBatch(id);
                return View(batchEntity);
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error() { Text = e.Message}));
            }
        }

        public async Task<IActionResult> Create(BatchGroupType groupType, int? routeId)
        {
            var batch = new BatchEntity()
            {
                GroupType = groupType,
                RouteId = routeId
            };
            await SetEditDropdownOptions(batch);
            return View("Edit", batch);
        }

        public async Task<IActionResult> Delete(int id, BatchGroupType groupType, int? routeId, int? warehouseId)
        {
            await _batchService.DeleteAsync(id);
            return RedirectToAction(nameof(Inventory), new {groupType, routeId, warehouseId});
        }

        private async Task SetEditDropdownOptions(BatchEntity batch)
        {
            var recipients = await _userService.ListAsync(new UserListFilterOptions()
            {
                Skip = 0,
                PageSize = int.MaxValue
            });
            ViewBag.Recipients = recipients.Items;
            ViewBag.Agents = await _userService.ListAgentsAsync();
            ViewBag.PickUpLocations = await _userService.ListPickUpLocationsAsync(2);
            ViewBag.ActionTypes = batch.GetActionTypes();
            ViewBag.ActionType = ViewBag.ActionTypes[0];
            if (batch.GroupType == BatchGroupType.LoadDelivery || !batch.RouteId.HasValue)
            {
                ViewBag.MasterBatches = new List<BatchEntity>();
            }
            else
            {
                ViewBag.MasterBatches = (await _batchService.ListMasterBatchesAsync(BatchGroupType.LoadDelivery, batch.RouteId));
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(BatchEntity model)
        {
            var result = await _batchService.SaveAsync(model);
            return RedirectToAction(nameof(Edit), new {id = result.Id });
        }

        [HttpPost]
        public async Task<IActionResult> SaveScanStatus(OrderScanStatusEntity model)
        {
            try
            {
                //_session.CurrentUser.Id
                var result = await _batchService.SaveOrderScanStatus(model, _session.CurrentUser.Id);
                return Json(new MethodResult<OrderScanStatusEntity>(result));
            }
            catch (ApiException e)
            {
                return Json(new MethodResult<OrderScanStatusEntity>(new Error()
                {
                    Name = e.Name,
                    Text = e.Message
                }));
            }
        }

        public async Task<IActionResult> DailyScanEditBox(int boxId, int? highlightOrderId = null)
        {
            var batchEntity = await _batchService.GetForEditBoxAsync(boxId);
            var box = batchEntity.Boxes.FirstOrDefault(b => b.Id == boxId);
            if (box != null && box.Orders != null && box.Orders.Any())
            {
                box.Orders = box.Orders.OrderByDescending(o => o.Status.Max(o => o.Date)).ToList();
                var scanStatusEntities = _batchService.GetOrderScanStatusEntities(box.Orders.Select(o => o.Id));
                foreach (var order in box.Orders)
                {
                    var scanStatus = scanStatusEntities.Where(s => s.OrderId == order.Id);
                    if (scanStatus.Any())
                    {
                        order.ScanStatusType = (OrderScanStatusType)scanStatus.Max(s => s.Status);
                    }
                }
            }
            ViewData["BoxId"] = boxId;
            return View(batchEntity);
        }



        public async Task<IActionResult> EditBox(int boxId, int? highlightOrderId = null)
        {
            var batchEntity = await _batchService.GetForEditBoxAsync(boxId);
            var box = batchEntity.Boxes.FirstOrDefault(b => b.Id == boxId);
            if (box != null && box.Orders != null && box.Orders.Any())
            {
                box.Orders = box.Orders.OrderByDescending(o => o.Status.Max(o => o.Date)).ToList();
                var scanStatusEntities = _batchService.GetOrderScanStatusEntities(box.Orders.Select(o => o.Id));
                foreach (var order in box.Orders)
                {
                    var scanStatus = scanStatusEntities.Where(s => s.OrderId == order.Id);
                    if (scanStatus.Any())
                    {
                        order.ScanStatusType = (OrderScanStatusType)scanStatus.Max(s => s.Status);
                    }
                }
            }
            ViewData["BoxId"] = boxId;
            return View(batchEntity);
        }

        [HttpPost]
        public async Task<JsonResult> AddOrder(int boxId, string orderNumber)
        {
            try
            {
                var order = await _orderService.FindAsync(orderNumber);

                if (order == null)
                {
                    await _batchService.AddOtherOrderAsync(boxId, orderNumber);
                }
                else
                {
                    await _batchService.AddOrderAsync(boxId, order.Id, order);
                    await _emailService.QueueEmailDataInWarehouseAsync(order.Id, _session.CurrentUser.Id, order.Creator.Id);
                }
                
                return Json(new MethodResult<OrderEntity>(order));
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return Json(new MethodResult<OrderEntity>(new Error() { Text = e.InnerException.Message }));
                }
                else
                {
                    return Json(new MethodResult<OrderEntity>(new Error() { Text = e.Message}));
                }
            }
        }

        public async Task<JsonResult> GetBatchCountByRoute(BatchGroupType groupType)
        {
            var result = await _batchService.GetBatchCountByRouteAsync(groupType);
            return Json(new MethodResult<IEnumerable<RouteBatchCount>>(result));
        }

        public async Task<JsonResult> GetOrderCostSummary(int batchId)
        {
            try
            {
                var result = await _batchService.GetOrderCostSummary(batchId);
                return Json(new MethodResult<OrderCostSummaryEntity>(result));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<OrderEntity>(new Error() { Text = e.Message}));
            }
        }

        public async Task<ActionResult> RemoveOrder(int boxId, int orderId)
        {
            await _batchService.RemoveOrderAsync(boxId, orderId);
            return RedirectToAction(nameof(EditBox), new {boxId});
        }

        public async Task<IActionResult> AddBox(int id, int boxNumber)
        {
            await _batchService.AddBoxAsync(id, boxNumber);
            return RedirectToAction(nameof(Edit), new {id});
        }

        public async Task<IActionResult> MoveNext(int id)
        {
            await _batchService.MoveNextAsync(id);
            return RedirectToAction(nameof(Edit), new {id});
        }

        public async Task<IActionResult> Split(int id)
        {
            await _batchService.SplitAsync(id);
            return RedirectToAction(nameof(Edit), new { id });
        }

        public async Task<IActionResult> SplitByNonAgent(int id)
        {
            try
            {
                await _batchService.SplitByNonAgent(id);
            }
            catch (Exception ex)
            {
                return BadRequest(Json(new MethodResult<object>(new Error
                {
                    Name = "SplitByNonAgentErrors",
                    Text = ex.Message
                })));
            }
            return Ok(new MethodResult<object>(null));
        }

        public async Task<IActionResult> SplitByNonLocation(int id)
        {
            try
            {
                await _batchService.SplitByNonLocation(id);
            }
            catch (Exception ex)
            {
                return BadRequest(Json(new MethodResult<object>(new Error
                {
                    Name = "SplitByNonLocationErrors",
                    Text = ex.Message
                })));
            }
            return Ok(new MethodResult<object>(null));
        }

        public async Task<IActionResult> SplitByLocations(int id)
        {
            try
            {
                await _batchService.SplitByLocationsAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(Json(new MethodResult<object>(new Error
                {
                    Name = "SplitByLocationsErrors",
                    Text = ex.Message
                })));
            }
            return Ok(new MethodResult<object>(null));
        }

        public async Task<IActionResult> SplitByRecipients(int id)
        {
            try
            {
                await _batchService.SplitByRecipientsAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(Json(new MethodResult<object>(new Error
                {
                    Name = "SplitByRecipientsErrors",
                    Text = ex.Message
                })));
            }
            return Ok(new MethodResult<object>(null));
        }
        
        public async Task<IActionResult> SplitByAgents(int id)
        {
            try
            {
                await _batchService.SplitByAgentsAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(Json(new MethodResult<object>(new Error
                {
                    Name = "SplitByAgentsErrors",
                    Text = ex.Message
                })));
            }
            return Ok(new MethodResult<object>(null));
        }


        [HttpPost("smsNotify")]
        public async Task<IActionResult> SmsNotify(int id, string pickUpLocation, string pickUpTime, string customMessage, string recipientPhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(pickUpLocation) || string.IsNullOrWhiteSpace(pickUpTime))
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = "MissingParameters",
                    Text = "请输入取货地址和取货时间。"
                }));
            }
            try
            {
                IEnumerable<SmsUserInfo> smsUserInfos = null;
                IEnumerable<SmsRequest> requests = null;
                if (!string.IsNullOrWhiteSpace(recipientPhoneNumber))
                {
                    var user = await _userService.GetAsync(recipientPhoneNumber);
                    if (user == null)
                    {
                        return Json(new MethodResult<IEnumerable<SmsUserInfo>>(new Error
                        {
                            Name = "NoUserForSms",
                            Text = $"没有电话为{recipientPhoneNumber}的用户。"
                        }));
                    }

                    var smsUserInfo = await _smsService.GetSmsUserInfoByUserIdAsync(user.Id);
                    smsUserInfos = new List<SmsUserInfo> { smsUserInfo };
                    requests = new List<SmsRequest>
                {
                    new SmsRequest
                    {
                        Message = string.IsNullOrWhiteSpace(customMessage) ? MessageUtils.BatchNotificationNewMessage : customMessage,
                        MobilePhoneNumber = smsUserInfo.MobilePhoneNumber,
                        OrderStartNumber = smsUserInfo.OrderStartNumber,
                        FullName = smsUserInfo.FullName,
                        BelongsTo = smsUserInfo.BelongsToName,
                        Level = smsUserInfo.Level
                    }
                };
                    await _smsService.SendAsync(requests, _session.CurrentUser.Id, id);
                }
                else
                {
                    smsUserInfos = await _smsService.GetSmsUserInfosByBatchIdAsync(id);
                    requests = smsUserInfos.Select(u => new SmsRequest
                    {
                        Message = string.IsNullOrWhiteSpace(customMessage) ? MessageUtils.BatchNotificationNewMessage : customMessage,
                        MobilePhoneNumber = u.MobilePhoneNumber,
                        OrderStartNumber = u.OrderStartNumber,
                        FullName = u.FullName,
                        BelongsTo = u.BelongsToName,
                        Level = u.Level
                    });
                    var emailData = await _context.EmailDatas.Select(e => e.BatchId).FirstOrDefaultAsync(i => i == id);
                    if (emailData == default)
                    {
                        await _context.Database.ExecuteSqlRawAsync($@"
INSERT INTO email_data (OrderId,SenderUserId,RecipientUserId,DateCreated,DateSent,BatchId)
SELECT OrderId, {_session.CurrentUser.Id}, o.CreatedById, NOW(), NULL, bb.BatchId FROM batch_box bb
JOIN batch_box_order_map bbom ON bb.Id=bbom.BatchBoxId
JOIN transport_order o ON bbom.OrderId=o.Id
WHERE BatchId={id}");
                    }

#pragma warning disable 4014
                    Task.Run(async () =>
                    {
                        await _smsService.SendSmsAndEmailAsync(_session.CurrentUser.Id, id, customMessage, pickUpLocation, pickUpTime);
                    })
                    .ConfigureAwait(false);
#pragma warning restore 4014
                }
                return Json(new MethodResult<IEnumerable<SmsUserInfo>>(smsUserInfos));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }

        public async Task<IActionResult> Pay(int id)
        {
            await _batchService.PayAndMoveNextAsync(id, PayType.Balance);
            return RedirectToAction(nameof(Edit), new { id });
        }

        public async Task<IActionResult> Commission(int id)
        {
            await _batchService.CommissionAsync(id);
            return RedirectToAction(nameof(Edit), new { id });
        }

        public async Task<IActionResult> PrintBatch(int id)
        {
            var batch = await _batchService.GetForPrintAsync(id);
            ViewData["ShippingAddress"] = "";
            ViewData["RecipientName"] = "";
            ViewData["RecipientPhone"] = "";
            if (batch.RecipientAddressId.HasValue)
            {
                var shippingAddress = await _context.SysShippingAddresses.FirstAsync(adr => adr.Id == batch.RecipientAddressId.Value);
                var address = shippingAddress.DetailArea + " " + shippingAddress.PostalCode;
                ViewData["ShippingAddress"] = address;
                var recipientName = shippingAddress.Consignee;
                ViewData["RecipientName"] = recipientName;
                var recipientPhone = shippingAddress.Mobile;
                ViewData["RecipientPhone"] = recipientPhone;
            }
            return View(batch);
        }

        public async Task<IActionResult> PrintBatchBox(int id)
        {
            var batch = await _batchService.GetByBoxIdAsync(id);
            var box = batch.Boxes.First(b => b.Id == id);
            var result = new BatchBoxPrintModel()
            {
                Id = id,
                Number = $"{batch.Id}-{box.Number}",
                BatchName = batch.Name,
                OrderCount = box.Orders.Count(),
                Weight = box.Orders.Sum(o => o.WeightKg)
            };
            return View(result);
        }

        public async Task<IActionResult> Export(int id)
        {
            var batch = await _batchService.GetAsync(id);
            var orders = batch.Boxes.SelectMany(bx => bx.Orders);
            
            using var result = _fileExportService.Export(orders, "haiyun");
            var wb = result as XLWorkbook;
            Response.Headers.Add("Set-Cookie", "fileDownload=true; path=/");
            return wb.Deliver(batch.Name + ".xlsx");
        }

        public async Task<JsonResult> UpdateLoadDeliveryProperties(int id)
        {
            try
            {
                await _batchService.UpdateOrdersLoadDeliveryProperties(id);
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost]
        public async Task<JsonResult> Merge(int targetBatchId, int sourceBatchId, int? sourceBoxNumber)
        {
            try
            {
                await _batchService.MergeAsync(targetBatchId, sourceBatchId, sourceBoxNumber);
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost]
        public async Task<JsonResult> SendMessage(int batchId, string message)
        {
            var batch = await _batchService.GetAsync(batchId);
            var sentUsers = new HashSet<int>();

            foreach (var order in batch.Boxes.SelectMany(b => b.Orders))
            {
                if (sentUsers.Contains(order.Creator.Id))
                {
                    continue;
                }

                sentUsers.Add(order.Creator.Id);
                await _notificationService.SendMessageAsync(order.Creator.CanadaPhoneNumber, message,
                    _session.CurrentUser.Name);
            }

            return Json(new MethodResult<bool>(true));
        }

        [HttpPost]
        public async Task<IActionResult> SetCouponBatchAnonymous(int id, bool anonymous)
        {
            var batch = await _context.CouponBatches.FirstOrDefaultAsync(b => b.Id == id);
            batch.Anonymous = anonymous;
            var coupons = _context.Coupons.Where(c => c.CouponBatchId == id);
            await coupons.ForEachAsync(c => c.CouponType = anonymous ? 1 : 2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EditCoupon), new {id = id });
        }

        [HttpPost]
        public async Task<IActionResult> SetCouponPrinted(int couponBatchId)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO coupon_status(CouponId, Status, DateCreated, UserId)
                    SELECT Id as CouponId, @p0 as Status, NOW() as DateCreated, @p1 as UserId FROM coupon WHERE CouponBatchId=@p2",
                    CouponStatusType.CouponPrinted, _session.CurrentUser.Id, couponBatchId
                );
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = "SetCouponPrinted",
                    Text = e.Message
                }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetCouponMailed(int couponBatchId)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO coupon_status(CouponId, Status, DateCreated, UserId)
                    SELECT Id as CouponId, @p0 as Status, NOW() as DateCreated, @p1 as UserId FROM coupon WHERE CouponBatchId=@p2",
                    CouponStatusType.CouponMailed, _session.CurrentUser.Id, couponBatchId
                );
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = "SetCouponMailed",
                    Text = e.Message
                }));
            }
        }

        public async Task<IActionResult> ExportCouponBatch(int id)
        {
            var couponBatch = await _couponService.GetAsync(id);
            
            using var result = _fileExportService.Export(couponBatch.Coupons, "coupon", couponBatch: couponBatch);
            var wb = result as XLWorkbook;
            Response.Headers.Add("Set-Cookie", "fileDownload=true; path=/");
            return wb.Deliver(couponBatch.Name + ".xlsx");
        }
        
        public async Task<IActionResult> DeleteCouponBatch(int id)
        {
            try
            {
                await _couponService.DeleteCouponBatchAsync(id);
                return RedirectToAction(nameof(CouponInventory));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = "DeleteCouponBatch",
                    Text = e.Message
                }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadCouponBatchPhoto(int couponBatchId, string photoData)
        {
            try
            {
                var entity = await _couponService.AddPhotoAsync(couponBatchId, photoData);
                return Json(new MethodResult<CouponBatchEntity>(entity));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<OrderPhotoEntity>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveCouponBatchEmailContent(int couponBatchId, string emailContent)
        {
            try
            {
                var batch = await _context.CouponBatches.FirstOrDefaultAsync(c => c.Id == couponBatchId);
                batch.EmailContent = emailContent;
                await _context.SaveChangesAsync();
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveCouponBatchSmsContent(int couponBatchId, string smsContent)
        {
            try
            {
                var batch = await _context.CouponBatches.FirstOrDefaultAsync(c => c.Id == couponBatchId);
                batch.SmsContent = smsContent;
                await _context.SaveChangesAsync();
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }
    }
}
