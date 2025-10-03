using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using Persistence.Utils;

namespace Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IChinaStatusService _chinaStatusService;
        private readonly IRouteService _routeService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IDateTime _date;
        private readonly IMemoryCache _memoryCache;
        private readonly IStorageService _storageService;
        private readonly ISmsService _smsService;
        private readonly ISystemSession _session;
        private readonly ICouponService _couponService;


        public OrderService(EplusDbContext context, IMapper mapper, IChinaStatusService chinaStatusService, ILogger<OrderService> logger, IDateTime date, IRouteService routeService, IUserService userService, IMemoryCache memoryCache, IStorageService storageService, ISmsService smsService, ISystemSession session, ICouponService couponService)
        {
            _context = context;
            _mapper = mapper;
            _chinaStatusService = chinaStatusService;
            _logger = logger;
            _date = date;
            _routeService = routeService;
            _userService = userService;
            _memoryCache = memoryCache;
            _storageService = storageService;
            _smsService = smsService;
            _session = session;
            _couponService = couponService;
        }

        public async Task<PagedResult<OrderEntity>> ListAsync(OrderListFilterOptions filterOptions)
        {
            var orders = _context.TransportOrders
                .Where(o => (o.IsFromChina)
                            && (!filterOptions.OrderState.HasValue || o.State == (int)filterOptions.OrderState.Value)
                            && (string.IsNullOrEmpty(filterOptions.OrderNumberToSearch) || o.OrderNumber.Contains(filterOptions.OrderNumberToSearch))
                            && (string.IsNullOrEmpty(filterOptions.DomesticNumberToSearch) || o.DomesticNumber.Contains(filterOptions.DomesticNumberToSearch))
                            && (string.IsNullOrEmpty(filterOptions.CreatorToSearch) || o.CreatedBy.OrderStartNumber.Equals(filterOptions.CreatorToSearch) || o.CreatedBy.Customer.Name.Equals(filterOptions.CreatorToSearch))
                )
                .Include(o => o.BatchBoxOrderMaps).ThenInclude(m => m.BatchBox).ThenInclude(box => box.Batch)
                .Include(o => o.OrderStatuses).ThenInclude(os => os.User).ThenInclude(u => u.Customer)
                .Include(o => o.PickUpLocation).ThenInclude(o => o.BelongsTo)
                .Include(o => o.CreatedBy).ThenInclude(u => u.Customer)
                .OrderByDescending(o => o.DateCreated);

            var total = await orders.CountAsync();
            var pagedOrders = orders.Skip(filterOptions.Skip).Take(filterOptions.PageSize);
            var items = await pagedOrders.Select(
                o => _mapper.Map<OrderEntity>(o)).ToListAsync();

            var result = new PagedResult<OrderEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }

        public void ClearCache(int id)
        {
            _memoryCache.Remove($"order-{id}");
        }

        public async Task<OrderEntity> GetAsync(int id)
        {
            if (!_memoryCache.TryGetValue($"order-{id}", out OrderEntity result))
            {
                var order = await FindAsync(o => o.Id == id);
                if (order == null)
                {
                    throw new Exception($"Order with id: {id} doesn't exist.");
                }

                result = _mapper.Map<OrderEntity>(order);
                _memoryCache.Set($"order-{id}", result, TimeSpan.FromMinutes(1));
            }

            return result;
        }

        public async Task AddStatus(OrderStatusType status, int operatorId, params OrderEntity[] orders)
        {
            var now = _date.UserNow;
            
            foreach (var order in orders)
            {
                var orderStatus = new OrderStatus()
                {
                    OrderId = order.Id,
                    DateCreated = now,
                    Status = (int)status,
                    UserId = operatorId
                };

                _memoryCache.Remove($"order-{order.Id}");
                await _context.OrderStatuses.AddAsync(orderStatus);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddInternalStatus(OrderStatusType status, int operatorId, params int[] orderIds)
        {
            var now = _date.UserNow;
            
            foreach (var orderId in orderIds)
            {
                var orderStatus = new OrderStatusInternal()
                {
                    OrderId = orderId,
                    DateCreated = now,
                    Status = (int)status,
                    UserId = operatorId
                };

                _memoryCache.Remove($"order-{orderId}");
                await _context.OrderInternalStatuses.AddAsync(orderStatus);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateChinaStatus()
        {
            var orders = await _context.TransportOrders.Include(o => o.OrderStatuses).Include(o => o.CreatedBy).Where(
                o => o.IsFromChina && o.OrderStatuses.All(s =>
                    (s.Status != (int) OrderStatusType.DeliveredToChinaWareHouse_LateConfirmation) &&
                    (s.Status != (int) OrderStatusType.DeliveredToChinaWareHouse_Error) &&
                    (s.Status != (int) OrderStatusType.DeliveredToChinaWareHouse_Pending))
            ).OrderByDescending(o => o.DateCreated).Take(2).ToListAsync();

            var updateTasks = orders.Select(GetChinaStatusAndUpdate);
            var t = Task.WhenAll(updateTasks);

            try
            {
                t.Wait();
            }
            catch (Exception e)
            {
                _logger.LogError("Error in updating china status", e);
            }

            await _context.SaveChangesAsync();
            return string.Join(", ", orders.Select(o => o.Id));
        }

        public async Task Delete(int id)
        {
            var order = _context.TransportOrders.Include(o => o.ChinaItems).Include(o => o.OrderStatuses)
                .Include(o => o.OrderActionHistories).Include(o => o.OrderBaggages)
                .Include(o => o.BatchBoxOrderMaps)
                .Include(o => o.OrderPhotos)
                .First(o => o.Id == id);

            foreach (var photo in order.OrderPhotos)
            {
                await _storageService.DeleteAsync(photo.Url.Substring(photo.Url.IndexOf("order")));
            }

            _context.BatchBoxOrderMaps.RemoveRange(order.BatchBoxOrderMaps);
            _context.OrderStatuses.RemoveRange(order.OrderStatuses);
            _context.OrderActionHistories.RemoveRange(order.OrderActionHistories);
            _context.OrderBaggages.RemoveRange(order.OrderBaggages);
            _context.ChinaItems.RemoveRange(order.ChinaItems);
            _context.OrderPhotos.RemoveRange(order.OrderPhotos);

            _context.TransportOrders.Remove(order);
            _memoryCache.Remove($"order-{order.Id}");
            await _context.SaveChangesAsync();
        }

        public async Task<OrderEntity> FindAsync(string number)
        {
            var order = await FindAsync(o => o.OrderNumber == number || o.DomesticNumber == number);
            if (order == null)
            {
                return null;
            }

            var result = _mapper.Map<OrderEntity>(order);
            return result;
        }

        public async Task<OrderEntity> SaveAsync(OrderEntity entity)
        {
            TransportOrder order;
            if (entity.Id == 0)
            {
                order = await CreateAsync(entity);
            }
            else
            {
                order = await UpdateAsync(entity);
            }

            return _mapper.Map<OrderEntity>(order);
        }

        public async Task SetOrderState(int id, OrderState state, string reason)
        {
            var order = await _context.TransportOrders
                .Include(o => o.OrderStatuses)
                .FirstAsync(o => o.Id == id);
            var originalState = order.State;
            order.State = (int)state;
            order.ActionReason = reason;
            _memoryCache.Remove($"order-{order.Id}");
            await _context.SaveChangesAsync();
            if (state == OrderState.Illegal && originalState == (int)OrderState.InWarehouse)
            {
                // 对于 "已入库" 的单，点击 "违禁品" 时，添加状态 "已发货" 和 后台操作记录 "违禁品"
                await AddStatus(OrderStatusType.Dispatched, _session.CurrentUser.Id, new OrderEntity { Id = order.Id });
                await AddInternalStatus(OrderStatusType.ForbiddenItem, _session.CurrentUser.Id, order.Id);
            }
            if (state == OrderState.PendingConfirmation)
            {
                // 对于 "已入库" 的单，点击 "待确认" 时，添加状态 "等待用户确认"
                if (originalState == (int)OrderState.InWarehouse)
                {
                    await AddStatus(OrderStatusType.PendingCustomerConfirm, _session.CurrentUser.Id, new OrderEntity { Id = order.Id });
                }
                try
                {
                    #pragma warning disable 4014
                    Task.Run(async () =>
                    {
                        var userId = _session.CurrentUser.Id;
                        var smsUserInfo = await _smsService.GetSmsUserInfoByUserIdAsync(order.CreatedById);
                        await _smsService.SendAsync(new SmsRequest[]
                        {
                            new SmsRequest
                            {
                                Message = MessageUtils.GetOrderPendingConfirmationNewMessage(),
                                MobilePhoneNumber = smsUserInfo.MobilePhoneNumber,
                                OrderStartNumber = smsUserInfo.OrderStartNumber,
                                BelongsTo = smsUserInfo.BelongsToName,
                                FullName = smsUserInfo.FullName,
                                Level = smsUserInfo.Level
                            }
                        }, userId);
                        if (smsUserInfo.Email != null)
                        {
                            var smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential("notification.eplus@gmail.com", "dybqcagazakncdqb"),
                                EnableSsl = true
                            };
                            
                            smtpClient.Send(
                                "notification.eplus@gmail.com",
                                smsUserInfo.Email,
                                MessageUtils.GetOrderPendingConfirmationEmailSubject(order.DomesticNumber),
                                MessageUtils.GetOrderPendingConfirmationEmailBody(order.DomesticNumber, reason)
                            );
                        }
                    })
                    .ConfigureAwait(false);
                    #pragma warning restore 4014
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " | " + e.StackTrace);
                }
            }
        }

        public async Task<int> BatchCreateOrderAsync(string prefix, string startNumber, string endNumber, int routeId, int batchId, string validFrom, string validUntil, decimal shippingCost, decimal minimumPrice)
        {
            int start = int.Parse(startNumber);
            int end = int.Parse(endNumber);
            int digits = endNumber.Length;
            var route = await _routeService.GetAsync(routeId);
            var coupons = new List<Coupon>();
            for (int i = start; i <= end; i++)
            {
                string postFix = i.ToString($"D{digits}");
                string orderNumber = GenerateOrderNumber(_session.CurrentUser, route) + postFix;
                string domesticNumber = Guid.NewGuid().ToString().ToUpperInvariant().Split("-").Last();
                var coupon = new Coupon
                {
                    CouponNumber = orderNumber,
                    DomesticNumber = domesticNumber,
                    CreatedById = _session.CurrentUser.Id,
                    CreateTime = DateTime.UtcNow,
                    ShippingCost = - Math.Abs(shippingCost),
                    MinimumPrice = minimumPrice,
                    CouponBatchId = batchId,
                    ValidFrom = string.IsNullOrWhiteSpace(validFrom) ? null : DateTime.Parse(validFrom),
                    ValidUntil = string.IsNullOrWhiteSpace(validUntil) ? null : DateTime.Parse(validUntil)
                };
                coupons.Add(coupon);
                _context.Coupons.Add(coupon);
            }
            await _context.SaveChangesAsync();

            _couponService.AddStatus(CouponStatusType.CouponCreated, _session.CurrentUser.Id, coupons.Select(c => c.Id));

            await _context.SaveChangesAsync();
            
            return end - start + 1;
        }

        public async Task<OrderEntity> SaveDraftAsync(OrderEntity entity)
        {
            if (!entity.RouteId.HasValue)
            {
                throw new ArgumentNullException("Route Id is null");
            }

            var route = await _routeService.GetAsync(entity.RouteId.Value);

            var order = new TransportOrder();
            order.CreatedById = entity.Creator.Id;
            order.DomesticNumber = entity.DomesticNumber;
            order.DomesticCarrier = entity.DomesticCarrier;
            order.OwnerId = entity.DraftById;
            order.OrderNumber = GenerateOrderNumber(entity.Creator, route);
            order.State = (int)entity.State;
            order.IsFromChina = true;
            order.DateCreated = _date.UserNow;
            order.RecipientId = entity.Creator.CustomerId;
            order.SenderId = entity.Creator.CustomerId;
            order.RouteId = entity.RouteId;
            order.PickUpLocationId = entity.PickUpLocationId;

            // 在 “运单管理” -> “未匹配” 中添加运单时，运单要一个初始的 “预创建运单” 状态。
            if (entity.State == OrderState.Draft)
            {
                order.OrderStatuses.Add(new OrderStatus
                {
                    DateCreated = DateTime.Now,
                    Status = (int)OrderStatusType.PreCreateOrder,
                    UserId = entity.Creator.Id
                });
            }

            var initialStatus = await GetInitialStatusAsync(entity);
            order.OrderStatuses.Add(_mapper.Map<OrderStatus>(initialStatus));

            await _context.TransportOrders.AddAsync(order);
            await _context.SaveChangesAsync();

            entity.Id = order.Id;
            return entity;
        }

        public async Task<decimal> CalculateItemCostAsync(OrderEntity order)
        {
            if (!order.RouteId.HasValue || !order.Items.Any())
            {
                return 0;
            }

            var route = await _routeService.GetAsync(order.RouteId.Value);
            var categoryMap = route.ItemPrices.ToDictionary(it => it.Item, it => it.Price);
            var mostValuablePrice = route.ItemPrices.Max(it => it.Price);
            var mostExpensiveItemPrice = order.Items.Select(it =>
                categoryMap.ContainsKey(it.Category)
                    ? categoryMap[it.Category]
                    : mostValuablePrice).Max();

            return mostExpensiveItemPrice * order.WeightKg + route.FixedPrice;
        }

        public async Task ReturnCompleteAsync(int id)
        {
            var order = await _context.TransportOrders.FirstAsync(o => o.Id == id);
            order.State = (int)OrderState.Returned;
            _memoryCache.Remove($"order-{order.Id}");
            await _context.SaveChangesAsync();
        }

        public async Task<OrderPhotoEntity> AddPhotoAsync(int orderId, string rawData)
        {
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            string random = RandomGenerator.RandomAlphaNumericString(11);
            string objectKey = $"order/{timestamp}{random}/id{orderId}.png";

            var photoUrl = await _storageService.UploadAsync(rawData, objectKey);
            await _context.OrderPhotos.AddAsync(new OrderPhoto
            {
                DateCreated = DateTime.UtcNow,
                OrderId = orderId,
                Url = photoUrl
            });
            await _context.SaveChangesAsync();
            return await Task.FromResult(new OrderPhotoEntity()
            {
                Id = orderId,
                Url = photoUrl
            });
        }

        public async Task DeletePhotoAsync(int photoId)
        {
            var photo = await _context.OrderPhotos.FirstOrDefaultAsync(p => p.Id == photoId);
            if (photo == null) return;
            await _storageService.DeleteAsync(photo.Url);
            _context.OrderPhotos.Remove(photo);
            await _context.SaveChangesAsync();
        }

        private async Task<TransportOrder> CreateAsync(OrderEntity entity)
        {
            if (!entity.RouteId.HasValue)
            {
                throw new ArgumentNullException("RouteId");
            }

            var route = await _routeService.GetAsync(entity.RouteId.Value);

            var order = new TransportOrder();
            order.CreatedById = entity.Creator.Id;
            order.OrderNumber = GenerateOrderNumber(entity.Creator, route);
            order.Route = route.Name;
            order.DomesticNumber = entity.DomesticNumber;
            order.DomesticCarrier = entity.DomesticCarrier;
            order.WeightKg = entity.WeightKg;
            if (entity.WeightKg > 0 && entity.PickUpLocationId.HasValue)
            {
                var pickUp = await _context.PickUpLocations.FirstOrDefaultAsync(p => p.Id == entity.PickUpLocationId);
                if (pickUp != null)
                {
                    order.DistrictAdditionalCost = entity.WeightKg * pickUp.DistrictAdditionalCost;
                }
            }
            order.ShippingCost = entity.ShippingCost;
            order.SecondTrackNumber = entity.IntNumber;
            order.SecondCarrier = entity.IntCarrier;
            order.Insurance = entity.Insurance;
            order.Duty = entity.Duty;
            order.ItemCost = entity.ItemCost;
            order.OversizeCost = entity.OversizeCost;
            order.FumigationCost = entity.FumigationCost;
            order.WarehouseCost = entity.WarehouseCost;
            order.PortMisCost = entity.PortMisCost;
            order.Discount = entity.Discount;
            order.StorageCost = entity.StorageCost;
            order.HiddenNotes = entity.WarehouseNotes;
            order.RouteId = entity.RouteId;
            order.OwnerId = entity.Creator.Id;
            order.State = (int)OrderState.Created;
            order.IsFromChina = true;
            order.DateCreated = _date.UserNow;
            order.RecipientId = entity.Creator.CustomerId;
            order.SenderId = entity.Creator.CustomerId;
            order.ChinaItems = entity.Items.Select(it => _mapper.Map<ChinaItem>(it)).ToList();
            order.OrderBaggages = entity.Baggages.Select(b => _mapper.Map<OrderBaggage>(b)).ToList();

            var initialStatus = await GetInitialStatusAsync(entity);
            order.OrderStatuses.Add(_mapper.Map<OrderStatus>(initialStatus));
            
            await _context.TransportOrders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private async Task<TransportOrder> UpdateAsync(OrderEntity entity)
        {
            var order = await _context.TransportOrders.FirstAsync(o => o.Id == entity.Id);

            if (entity.RouteId.HasValue && order.RouteId != entity.RouteId)
            {
                var route = await _routeService.GetAsync(entity.RouteId.Value);
                order.OrderNumber = GenerateOrderNumber(entity.Creator, route);
                order.Route = route.Name;
            }
            order.DomesticNumber = entity.DomesticNumber;
            order.DomesticCarrier = entity.DomesticCarrier;
            order.WeightKg = entity.WeightKg;
            order.ShippingCost = entity.ShippingCost;
            order.SecondTrackNumber = entity.IntNumber;
            order.SecondCarrier = entity.IntCarrier;
            order.Insurance = entity.Insurance;
            order.Duty = entity.Duty;
            order.ItemCost = entity.ItemCost;
            order.OversizeCost = entity.OversizeCost;
            order.FumigationCost = entity.FumigationCost;
            order.WarehouseCost = entity.WarehouseCost;
            order.PortMisCost = entity.PortMisCost;
            order.Discount = entity.Discount;
            order.StorageCost = entity.StorageCost;
            order.HiddenNotes = entity.WarehouseNotes;
            order.RouteId = entity.RouteId;
            if (entity.DistrictAdditionalCost > 0)
            {
                order.DistrictAdditionalCost = entity.DistrictAdditionalCost;
            }

            await UpdateItems(entity, order);
            await UpdateBaggages(entity, order);

            await _context.SaveChangesAsync();

            _memoryCache.Remove($"order-{order.Id}");
            return order;
        }

        private async Task UpdateItems(OrderEntity entity, TransportOrder order)
        {
            // remove items
            var itemsToRemove =
                _context.ChinaItems.Where(it => it.OrderId == order.Id && entity.Items.Select(i => i.Id).All(i => i != it.Id));
            _context.ChinaItems.RemoveRange(itemsToRemove);

            // insert items
            var itemsToAdd = entity.Items.Where(it => it.Id == 0).Select(it => _mapper.Map<ChinaItem>(it)).ToList();
            foreach (var chinaItem in itemsToAdd)
            {
                chinaItem.OrderId = order.Id;
            }

            await _context.ChinaItems.AddRangeAsync(itemsToAdd);

            // update items
            var itemsToUpdate = entity.Items.Where(it => it.Id > 0).ToList();
            foreach (var orderItem in itemsToUpdate)
            {
                var dbItem = await _context.ChinaItems.FirstAsync(it => it.Id == orderItem.Id);
                dbItem.ChineseName = orderItem.Name;
                dbItem.Brand = orderItem.Brand;
                dbItem.Material = orderItem.Material;
                dbItem.Category = orderItem.Category;
                dbItem.ClaimPrice = orderItem.ClaimPrice;
                dbItem.Quantity = orderItem.Quantity;
            }
        }

        private async Task UpdateBaggages(OrderEntity entity, TransportOrder order)
        {
            // remove baggages
            var itemsToRemove =
                _context.OrderBaggages.Where(it => it.OrderId == order.Id && entity.Baggages.Select(i => i.Id).All(i => i != it.Id));
            _context.OrderBaggages.RemoveRange(itemsToRemove);

            // insert baggages
            var itemsToAdd = entity.Baggages.Where(it => it.Id == 0).Select(it => _mapper.Map<OrderBaggage>(it)).ToList();
            foreach (var item in itemsToAdd)
            {
                item.OrderId = order.Id;
            }

            await _context.OrderBaggages.AddRangeAsync(itemsToAdd);

            // update baggages
            var itemsToUpdate = entity.Baggages.Where(it => it.Id > 0).Select(it => _mapper.Map<OrderBaggage>(it)).ToList();
            foreach (var item in itemsToUpdate)
            {
                _context.Attach(item);
                item.OrderId = order.Id;
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        private async Task<OrderEntity> FindAsync(Expression<Func<TransportOrder, bool>> searchCriteria)
        {
            var order = await _context.TransportOrders
                .Include(o => o.RouteNavigation)
                .Include(o => o.OrderBaggages)
                .Include(o => o.ChinaItems)
                .Include(o => o.OrderStatuses).ThenInclude(os =>os.User).ThenInclude(u => u.Customer)
                .Include(o => o.OrderInternalStatuses).ThenInclude(os => os.User).ThenInclude(u => u.Customer)
                .Include(o => o.OrderPhotos)
                .Include(o => o.PickUpLocation)
                .FirstOrDefaultAsync(searchCriteria);

            if (order == null)
            {
                return null;
            }

            var result = _mapper.Map<OrderEntity>(order);
            result.Creator = await _userService.GetAsync(order.CreatedById);

            // order created in regular site has to calculate item cost by us
            if (!order.IsItemCostUpdated)
            {
                var itemCost = await CalculateItemCostAsync(result);
                result.ItemCost = itemCost;
                result.ShippingCost += itemCost;
                
                order.ItemCost = itemCost;
                order.ShippingCost = result.ShippingCost;
                order.IsItemCostUpdated = true;
                await _context.SaveChangesAsync();
            }

            if (order.RecipientAddressId != null)
            {
                result.RecipientAddress = await _userService.GetShippingAddressAsync(order.RecipientAddressId.Value);
            }

            return result;
        }

        private async Task GetChinaStatusAndUpdate(TransportOrder order)
        {
            var status = await _chinaStatusService.GetStatusAsync(order.DomesticCarrier, order.DomesticNumber, order.CreatedBy.CanadaPhoneNumber);
            if (status.State == 3)
            {
                order.OrderStatuses.Add(new OrderStatus()
                {
                    DateCreated = _date.UserNow,
                    Status = (int)OrderStatusType.DeliveredToChinaWareHouse_Pending,
                });

                _memoryCache.Remove($"order-{order.Id}");
            }
        }

        private async Task<OrderStatusEntity> GetInitialStatusAsync(OrderEntity order)
        {
            var chinaOrder = await _chinaStatusService.GetStatusAsync(order.DomesticCarrier, order.DomesticNumber, order.Creator.CanadaPhoneNumber);

            var statusCode = chinaOrder.State switch
            {
                null when chinaOrder.Data.Count == 0 => OrderStatusType.InvalidDomesticNumber,
                3 => OrderStatusType.DeliveredToChinaWareHouse_Error,
                _ => OrderStatusType.InChinaTransit
            };

            return new OrderStatusEntity()
            {
                Date = _date.UserNow.AddSeconds(2),
                Status = statusCode,
                Operator = order.Creator
            };
        }

        private string GenerateOrderNumber(UserEntity user, RouteEntity route)
        {
            var lapsedSeconds = (ulong)Math.Round((_date.UserNow - _date.OrderStartTime).TotalSeconds);
            return route.Code + user.Code + lapsedSeconds.ToString().PadLeft(10, '0');
        }
    }
}
