using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Extensions;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Data;
using Persistence.Utils;
using System.Net;
using System.Net.Mail;
using Domain;

namespace Persistence.Services
{
    public class BatchService : IBatchService
    {
        private readonly static Dictionary<BatchStageType, OrderStatusType> STAGE_TO_STATUS = new Dictionary<BatchStageType, OrderStatusType>
        {
            { BatchStageType.LoadDelivery, OrderStatusType.PendingDeparture },
            { BatchStageType.Sailing, OrderStatusType.InTransit },
            { BatchStageType.Clearing, OrderStatusType.ArrivedAtDestinationHarbour },
            { BatchStageType.Sorting, OrderStatusType.ArrivedAtWarehouse },
            { BatchStageType.PendingPickUp, OrderStatusType.PendingPickup }
        };

        private readonly EplusDbContext _context;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IWarehouseService _warehouseService;
        private readonly IDateTime _date;
        private readonly IMapper _mapper;
        private readonly ISystemSession _session;
        private readonly IRouteService _routeService;
        private readonly ISmsService _smsService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogService _logService;

        public BatchService(EplusDbContext context, IDateTime date, IMapper mapper, IOrderService orderService, IUserService userService, IWarehouseService warehouseService, ISystemSession session, IRouteService routeService, ISmsService smsService, IMemoryCache memoryCache, ILogService logService)
        {
            _context = context;
            _date = date;
            _mapper = mapper;
            _orderService = orderService;
            _userService = userService;
            _warehouseService = warehouseService;
            _session = session;
            _routeService = routeService;
            _memoryCache = memoryCache;
            _smsService = smsService;
            _logService = logService;
        }

        public async Task<PagedResult<BatchEntity>> ListLoadDeliveryBatchAsync(BatchListFilterOptions filterOptions)
        {
            var batchesFiltered = _context.Batches
                .Include(b => b.LoadDeliveryBatches)
                .Where(b => b.LoadDeliveryBatches.Count > 0 && b.LoadDeliveryBatches.First().WarehouseId == filterOptions.WarehouseId
                    && (filterOptions.GroupType == BatchGroupType.LoadDelivery)
                    && (!filterOptions.Ids.Any() || filterOptions.Ids.Contains(b.Id)) && b.CompanyId == Config.COMPANY_ID)
                .Include(b => b.BatchBoxes)
                    .ThenInclude(bx => bx.BatchBoxOrderMaps)
                        .ThenInclude(m => m.Order)
                            .ThenInclude(o => o.CreatedBy)
                                .ThenInclude(o => o.BelongsToNavigation);

            IOrderedQueryable<Batch> batches = batchesFiltered.OrderByDescending(b => b.DateCreated);

            var total = await batches.CountAsync();
            var pagedBatches = batches.Skip(filterOptions.Skip);

            if (filterOptions.PageSize > 0)
            {
                pagedBatches = pagedBatches.Take(filterOptions.PageSize);
            }

            var itemsQuery = pagedBatches.Select(o => _mapper.Map<BatchEntity>(o));
            var items = await itemsQuery.ToListAsync();

            var result = new PagedResult<BatchEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }


        public async Task<PagedResult<BatchEntity>> ListWarehouseReceiveBatchAsync(BatchListFilterOptions filterOptions)
        {
            var batchesFiltered = _context.Batches
                .Include(b => b.BatchWarehouseReceives)
                .Where(b => b.BatchWarehouseReceives.Count > 0 && b.BatchWarehouseReceives.First().WarehouseId == filterOptions.WarehouseId
                    && (filterOptions.GroupType == BatchGroupType.WarehouseReceive)
                    && (!filterOptions.Ids.Any() || filterOptions.Ids.Contains(b.Id)) && b.CompanyId == Config.COMPANY_ID)
                .Include(b => b.BatchBoxes)
                    .ThenInclude(bx => bx.BatchBoxOrderMaps)
                        .ThenInclude(m => m.Order)
                            .ThenInclude(o => o.CreatedBy)
                                .ThenInclude(o => o.BelongsToNavigation);

            IOrderedQueryable<Batch> batches = batchesFiltered.OrderByDescending(b => b.DateCreated);

            var total = await batches.CountAsync();
            var pagedBatches = batches.Skip(filterOptions.Skip);

            if (filterOptions.PageSize > 0)
            {
                pagedBatches = pagedBatches.Take(filterOptions.PageSize);
            }

            var itemsQuery = pagedBatches.Select(o => _mapper.Map<BatchEntity>(o));
            var items = await itemsQuery.ToListAsync();

            var result = new PagedResult<BatchEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }


        public async Task<PagedResult<BatchEntity>> ListPalletBatchAsync(BatchListFilterOptions filterOptions)
        {
            var batchesFiltered = _context.Batches
                .Include(b => b.BatchPallets)
                .Where(b => b.BatchPallets.Count > 0 && b.BatchPallets.First().WarehouseId == filterOptions.WarehouseId
                    && (filterOptions.GroupType == BatchGroupType.Pallet)
                    && (!filterOptions.Ids.Any() || filterOptions.Ids.Contains(b.Id)) && b.CompanyId == Config.COMPANY_ID)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(o => o.BelongsToNavigation)
                .Include(b => b.BatchBoxMaps).ThenInclude(m => m.BatchBox).ThenInclude(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order);

            IOrderedQueryable<Batch> batches = batchesFiltered.OrderByDescending(b => b.DateCreated);
            
            var total = await batches.CountAsync();
            var pagedBatches = batches.Skip(filterOptions.Skip);

            if (filterOptions.PageSize > 0)
            {
                pagedBatches = pagedBatches.Take(filterOptions.PageSize);
            }

            var itemsQuery = pagedBatches.Select(o => _mapper.Map<BatchEntity>(o));
            var items = await itemsQuery.ToListAsync();

            var result = new PagedResult<BatchEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }

        public async Task<PagedResult<BatchEntity>> ListAsync(BatchListFilterOptions filterOptions)
        {
            var batchesFiltered = _context.Batches
                .Where(b => b.IsFromChina
                && (!filterOptions.GroupType.HasValue || (int)filterOptions.GroupType.Value == b.GroupType)
                && (!filterOptions.WarehouseId.HasValue || filterOptions.WarehouseId == b.WarehouseId)
                && (!filterOptions.RouteId.HasValue || filterOptions.RouteId == b.RouteId)
                && (!filterOptions.Ids.Any() || filterOptions.Ids.Contains(b.Id))
                && (!filterOptions.RecipientIds.Any() || (b.RecipientUserId != null && filterOptions.RecipientIds.Contains(b.RecipientUserId.Value)))
                && (!filterOptions.BelongsToUserIds.Any() || (b.BelongsToUserId != null && filterOptions.BelongsToUserIds.Contains(b.BelongsToUserId.Value)))
                && (filterOptions.GroupType == BatchGroupType.DailyScan || b.CompanyId == Config.COMPANY_ID)
                )
                .Include(b => b.BatchBoxes)
                    .ThenInclude(bx => bx.BatchBoxOrderMaps)
                        .ThenInclude(m => m.Order)
                            .ThenInclude(o => o.CreatedBy)
                                .ThenInclude(o => o.BelongsToNavigation)
                .Include(b => b.LoadDeliveryBatches);
            
            IOrderedQueryable<Batch> batches;
            if (filterOptions.GroupType.HasValue)
            {
                if (filterOptions.GroupType == BatchGroupType.PendingDispatch)
                {
                    batches = batchesFiltered.OrderBy(b => b.BatchBoxes.Sum(box => box.BatchBoxOrderMaps.Count));
                }
                else
                {
                    batches = batchesFiltered.OrderByDescending(b => b.DateCreated);
                }
            }
            else
            {
                batches = batchesFiltered.OrderByDescending(b => b.DateCreated);
            }

            var total = await batches.CountAsync();
            var pagedBatches = batches.Skip(filterOptions.Skip);

            if (filterOptions.PageSize > 0)
            {
                pagedBatches = pagedBatches.Take(filterOptions.PageSize);
            }
            foreach (var b in pagedBatches)
            {
                foreach (var box in b.BatchBoxes)
                {
                    box.BatchBoxOrderMaps = box.BatchBoxOrderMaps.Where(bbom => bbom.Order.CompanyId == Config.COMPANY_ID).ToList();
                }
            }

            var itemsQuery = pagedBatches.Select(o => _mapper.Map<BatchEntity>(o));
            var items = await itemsQuery.ToListAsync();

            var result = new PagedResult<BatchEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }

        public async Task<IEnumerable<BatchEntity>> ListMasterBatchesAsync(BatchGroupType groupType, int? routeId)
        {
            var batches = await _context.Batches
            .Where(b => b.IsFromChina
                && ((int)groupType == b.GroupType)
                && (!routeId.HasValue || routeId == b.RouteId) && b.CompanyId == Config.COMPANY_ID)
            .Select(b => new Batch
            {
                Id = b.Id,
                Name = b.Name
            }).ToArrayAsync();
            return batches.Select(b => _mapper.Map<BatchEntity>(b));
        }

        public async Task<PagedResult<BatchOtherOrderEntity>> ListOtherOrderAsync(
            BatchListOtherOrderFilterOptions filterOptions)
        {
            var batches = _context.BatchOtherOrders.Include(b => b.Batch)
                .Where(o => string.IsNullOrWhiteSpace(filterOptions.Number) ||
                            o.OtherOrder.Contains(filterOptions.Number)).OrderByDescending(b => b.Batch.DateCreated);

            var total = await batches.CountAsync();
            var pagedBatches = batches.Skip(filterOptions.Skip);

            if (filterOptions.PageSize > 0)
            {
                pagedBatches = pagedBatches.Take(filterOptions.PageSize);
            }

            var itemsQuery = pagedBatches.Select(o => new BatchOtherOrderEntity()
            {
                BatchId = o.BatchId,
                BatchName = o.Batch.Name,
                OtherOrder = o.OtherOrder

            });
            var items = await itemsQuery.ToListAsync();

            var result = new PagedResult<BatchOtherOrderEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }

        public async Task<IEnumerable<RouteBatchCount>> GetBatchCountByRouteAsync(BatchGroupType groupType)
        {
            // TODO: cache
            var routes = await _routeService.ListAsync();
            var result = routes.Select(r => new RouteBatchCount()
            {
                RouteId = r.Id,
                BatchCount = 0
            }).ToList();
            var threeMonthAgoFromToday = DateTime.Now.AddMonths(-3);
            var counts = await _context.Batches.Include(b => b.Route)
                .Where(b => b.IsFromChina && b.DateCreated >= threeMonthAgoFromToday &&
                            b.GroupType == (int)groupType && b.RouteId.HasValue && b.Route.CompanyId == Config.COMPANY_ID).GroupBy(b => b.RouteId).Select(pair =>
                    new RouteBatchCount()
                    {
                        RouteId = pair.Key.Value,
                        BatchCount = pair.Count()
                    }).ToListAsync();
            foreach (var routeBatchCount in counts)
            {
                var route = result.First(r => r.RouteId == routeBatchCount.RouteId);
                route.BatchCount = routeBatchCount.BatchCount;
            }
            return result;            
        }

        public async Task<OrderCostSummaryEntity> GetOrderCostSummary(int batchId)
        {
            OrderCostSummaryEntity entity = new OrderCostSummaryEntity{ BatchId = batchId };
            using (var conn = _context.Database.GetDbConnection())
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @$"
SELECT SUM(ItemCost) TotalItemCost, SUM(OversizeCost) TotalOversizeCost, SUM(WarehouseCost) TotalWarehouseCost,
SUM(FumigationCost) TotalFumigationCost, SUM(PortMisCost) TotalPortMisCost, COALESCE(SUM(InsuranceCost), 0) TotalInsuranceCost,
SUM(Duty) TotalDuty, SUM(StorageCost) TotalStorageCost, SUM(DistrictAdditionalCost) TotalDistrictAdditionalCost, SUM(Discount) TotalDiscount
FROM batch_box bb JOIN batch_box_order_map bbom ON bb.Id=bbom.BatchBoxId
JOIN transport_order o ON bbom.OrderId=o.Id
WHERE bb.BatchId=@batchId
                    ";
                    var param1 = command.CreateParameter();
                    param1.ParameterName = "@batchId";
                    param1.Value = batchId;
                    param1.DbType = System.Data.DbType.Int32;
                    command.Parameters.Add(param1);
                    var result = await command.ExecuteReaderAsync();
                    while (result.Read())
                    {
                        if (result[0] != DBNull.Value)
                        {
                            entity.TotalItemCost = result.GetDecimal(0);
                            entity.TotalOversizeCost = result.GetDecimal(1);
                            entity.TotalWarehouseCost = result.GetDecimal(2);
                            entity.TotalFumigationCost = result.GetDecimal(3);
                            entity.TotalPortMisCost = result.GetDecimal(4);
                            entity.TotalInsurance = result.GetDecimal(5);
                            entity.TotalDuty = result.GetDecimal(6);
                            entity.TotalStorageCost = result.GetDecimal(7);
                            entity.TotalDistrictAdditionalCost = result.GetDecimal(8);
                            entity.TotalDiscount = result.GetDecimal(9);
                        }
                    }
                }
            }
            return entity;
        }

        public async Task<IEnumerable<BatchEntity>> GetByOrderAsync(int orderId)
        {
            // Caller needs batch.Id, batch.Name, batch.Box.Id, batch.Box.Number
            var batches = await _context.BatchBoxOrderMaps
                .Include(bbom => bbom.BatchBox).ThenInclude(bb => bb.Batch)
                .Where(o => o.OrderId == orderId)
                .Select(bbom => new Batch
                {
                    Id = bbom.BatchBox.Batch.Id,
                    Name = bbom.BatchBox.Batch.Name,
                    BatchBoxes = new BatchBox[]
                    {
                        new BatchBox
                        {
                            Id = bbom.BatchBoxId,
                            Number = bbom.BatchBox.Number
                        }
                    }
                })
                .ToArrayAsync();
            IEnumerable<BatchEntity> entities = batches.Select(b => _mapper.Map<BatchEntity>(b));
            return entities;
        }

        private async Task<BatchEntity> GetAsyncForMerge(int id)
        {
            // Need all boxes' order Id, box's max number
            var batch = await _context.Batches.Include(b => b.BatchBoxes)
                .ThenInclude(bx => bx.BatchBoxOrderMaps)
                .ThenInclude(m => m.Order)
            .Select(b => new Batch
            {
                Id = b.Id,
                BatchBoxes = b.BatchBoxes.Select(bx => new BatchBox
                {
                    Id = bx.Id,
                    Number = bx.Number,
                    BatchBoxOrderMaps = bx.BatchBoxOrderMaps.Select(m => new BatchBoxOrderMap
                    {
                        Order = new TransportOrder
                        {
                            Id = m.OrderId
                        }
                    }).ToList() 
                }).ToList()

            })
            .FirstAsync(b => b.Id == id);
            var result = _mapper.Map<BatchEntity>(batch);
            return result;
        }

        private async Task<BatchEntity> GetAsyncForSplit(int id)
        {
            // Need batch.Id, batch.Name, batch.RouteId, batch.MasterBatchId, batch.GroupType, batch all boxes' orders
            // batch.Route.Type, order.pickuplocation.owner, order.creator.belongsto, order.creator.belongstoid, order.creator 
            var batchSQL = _context.Batches
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.BelongsToNavigation).ThenInclude(u => u.Customer)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.Customer)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.PickUpLocationNavigation).ThenInclude(u => u.BelongsTo)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.PickUpLocation).ThenInclude(p => p.BelongsTo)
                .Include(b => b.Route);
            var batch = await batchSQL.FirstAsync(b => b.Id == id && b.CompanyId == Config.COMPANY_ID);
            var result = _mapper.Map<BatchEntity>(batch);
            return result;
        }

        private async Task<BatchEntity> GetAsyncForSplitByLocations(int id)
        {
            // Need batch.Id, batch.Name, batch.RouteId, batch.MasterBatchId, batch.GroupType, batch.Stage, batch all boxes' orders
            // batch.Route.Type, order.pickuplocation.owner 
            IQueryable<Batch> batchSQL = _context.Batches
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.PickUpLocation).ThenInclude(p => p.BelongsTo)
                .Include(b => b.Route)
                .Where(b => b.Id == id && b.CompanyId == Config.COMPANY_ID)
                .Select(b => new Batch
                {
                    Id = b.Id,
                    Name = b.Name,
                    RouteId = b.RouteId,
                    MasterBatchId = b.MasterBatchId,
                    GroupType = b.GroupType,
                    Stage = b.Stage,
                    Route = b.Route == null ? null : new Route
                    {
                        Type = b.Route.Type
                    },
                    BatchBoxes = b.BatchBoxes.Select(bx => new BatchBox
                    {
                        Number = bx.Number,
                        BatchBoxOrderMaps = bx.BatchBoxOrderMaps.Select(m => new BatchBoxOrderMap
                        {
                            Order = new TransportOrder
                            {
                                Id = m.OrderId,
                                OrderNumber = m.Order.OrderNumber,
                                PickUpLocationId = m.Order.PickUpLocationId,
                                PickUpLocation = m.Order.PickUpLocation
                            }
                        }).ToList() 
                    }).ToList()
                });
            var batch = await batchSQL.FirstAsync();
            var result = _mapper.Map<BatchEntity>(batch);
            return result;
        }

        private async Task<BatchEntity> GetAsyncForSplitByNonLocation(int id)
        {
            // Need batch.Id, batch.Name, batch.RouteId, batch.MasterBatchId, batch.GroupType, batch.Stage, batch all boxes' orders
            // batch.Route.Type, order.PickUpLocation, order.PickUpLocationId, order.Creator.BelongsTo
            IQueryable<Batch> batchSQL = _context.Batches
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.PickUpLocation)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.BelongsToNavigation)
                .Include(b => b.Route)
                .Select(b => new Batch
                {
                    Id = b.Id,
                    Name = b.Name,
                    RouteId = b.RouteId,
                    MasterBatchId = b.MasterBatchId,
                    GroupType = b.GroupType,
                    Stage = b.Stage,
                    Route = new Route
                    {
                        Type = b.Route.Type
                    },
                    BatchBoxes = b.BatchBoxes.Select(bx => new BatchBox
                    {
                        Number = bx.Number,
                        BatchBoxOrderMaps = bx.BatchBoxOrderMaps.Select(m => new BatchBoxOrderMap
                        {
                            Order = new TransportOrder
                            {
                                Id = m.OrderId,
                                OrderNumber = m.Order.OrderNumber,
                                PickUpLocationId = m.Order.PickUpLocationId,
                                PickUpLocation = m.Order.PickUpLocation,
                                CreatedBy = m.Order.CreatedBy
                            }
                        }).ToList()
                    }).ToList()
                });
            var batch = await batchSQL.FirstAsync(b => b.Id == id && b.CompanyId == Config.COMPANY_ID);
            var result = _mapper.Map<BatchEntity>(batch);
            return result;
        }

        private async Task<BatchEntity> GetAsyncForSplitByRecipients(int id)
        {
            // Need batch.Id, batch.Name, batch.RouteId, batch.MasterBatchId, batch.GroupType, batch.Stage, batch all boxes' orders
            // batch.Route.Type, order.Creator
            IQueryable<Batch> batchSQL = _context.Batches
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.PickUpLocation).ThenInclude(p => p.BelongsTo)
                .Include(b => b.Route)
                .Where(b => b.Id == id && b.CompanyId == Config.COMPANY_ID)
                .Select(b => new Batch
                {
                    Id = b.Id,
                    Name = b.Name,
                    RouteId = b.RouteId,
                    MasterBatchId = b.MasterBatchId,
                    GroupType = b.GroupType,
                    Stage = b.Stage,
                    Route = b.Route == null ? null : new Route
                    {
                        Type = b.Route.Type
                    },
                    BatchBoxes = b.BatchBoxes.Select(bx => new BatchBox
                    {
                        Id = bx.Id,
                        Number = bx.Number,
                        BatchBoxOrderMaps = bx.BatchBoxOrderMaps.Select(m => new BatchBoxOrderMap
                        {
                            Order = new TransportOrder
                            {
                                Id = m.OrderId,
                                OrderNumber = m.Order.OrderNumber,
                                CreatedById = m.Order.CreatedById,
                                CreatedBy = m.Order.CreatedBy
                            }
                        }).ToList() 
                    }).ToList()
                });
            var batch = await batchSQL.FirstAsync();
            var result = _mapper.Map<BatchEntity>(batch);
            return result;
        }

        
        private async Task<BatchEntity> GetAsyncForSplitByAgents(int id)
        {
            // Need batch.Id, batch.Name, batch.RouteId, batch.MasterBatchId, batch.GroupType, batch.Stage, batch all boxes' orders
            // batch.Route.Type, order.Creator, order.Creator
            IQueryable<Batch> batchSQL = _context.Batches
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.PickUpLocation).ThenInclude(p => p.BelongsTo)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.BelongsToNavigation)
                .Include(b => b.Route)
                .Select(b => new Batch
                {
                    Id = b.Id,
                    Name = b.Name,
                    RouteId = b.RouteId,
                    MasterBatchId = b.MasterBatchId,
                    GroupType = b.GroupType,
                    Stage = b.Stage,
                    Route = new Route
                    {
                        Type = b.Route.Type
                    },
                    BatchBoxes = b.BatchBoxes.Select(bx => new BatchBox
                    {
                        Number = bx.Number,
                        BatchBoxOrderMaps = bx.BatchBoxOrderMaps.Select(m => new BatchBoxOrderMap
                        {
                            Order = new TransportOrder
                            {
                                Id = m.OrderId,
                                OrderNumber = m.Order.OrderNumber,
                                CreatedById = m.Order.CreatedById,
                                CreatedBy = m.Order.CreatedBy
                            }
                        }).ToList() 
                    }).ToList()
                });
            var batch = await batchSQL.FirstAsync(b => b.Id == id && b.CompanyId == Config.COMPANY_ID);
            var result = _mapper.Map<BatchEntity>(batch);
            return result;
        }

        private async Task<BatchEntity> GetAsyncForMoveNext(int id)
        {
            // Need batch.GroupType, batch.Name, batch.Route, batch.Box, batch.Box.Order, batch.Box.Order.Status
            // batch.RecipientId, batch.WeightKg, batch.TotalExpense
            var batch = await _context.Batches
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order).ThenInclude(o => o.OrderStatuses)
                .Include(b => b.Route)
                .FirstAsync(b => b.Id == id && b.CompanyId == Config.COMPANY_ID);
            return _mapper.Map<BatchEntity>(batch);
        }

        public async Task<BatchEntity> GetAsync(int id)
        {
            if (!_memoryCache.TryGetValue($"batch-{id}", out BatchEntity result))
            {
                var batch = await _context.Batches.Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.BelongsToNavigation).ThenInclude(u => u.Customer)
                    .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.Customer)
                    .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.PickUpLocationNavigation).ThenInclude(u => u.BelongsTo)
                    .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.PickUpLocation).ThenInclude(p => p.BelongsTo)
                    .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.ChinaItems)
                    .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.OrderStatuses)
                    .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .ThenInclude(m => m.Order).ThenInclude(o => o.OrderBaggages)
                    .Include(b => b.MasterBatch).ThenInclude(m => m.Progress).ThenInclude(p => p.Route)
                    .Include(b => b.Route)
                    .Include(b => b.User).ThenInclude(u => u.Customer)
                    .Include(b => b.BatchOtherOrders)
                    .FirstAsync(b => b.Id == id);

                result = _mapper.Map<BatchEntity>(batch);

                if (batch.RecipientUserId.HasValue)
                {
                    result.Recipient = await _userService.GetAsync(batch.RecipientUserId.Value);
                }

                if (batch.BelongsToUserId.HasValue)
                {
                    result.Agent = await _userService.GetAsync(batch.BelongsToUserId.Value);
                }

                _memoryCache.Set($"batch-{id}", result, TimeSpan.FromMinutes(10));
            }

            return result;
        }

        public async Task<BatchEntity> GetForPayAsync(int id)
        {
            // Need Id, GroupType, Agent.Balance, Agent.Id, Recipient.Balance, Recipient.Id,
            // TotalExpense, Route.Type, Boxes, PickUpLocation.BelongsTo
            var batch = await _context.Batches
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order).ThenInclude(o => o.OrderStatuses)
                .Include(b => b.Route)
                .Include(b => b.PickUpLocation).ThenInclude(p => p.BelongsTo)
                .FirstAsync(b => b.Id == id && b.CompanyId == Config.COMPANY_ID);

            var result = _mapper.Map<BatchEntity>(batch);

            if (batch.RecipientUserId.HasValue)
            {
                result.Recipient = await _userService.GetAsync(batch.RecipientUserId.Value);
            }

            if (batch.BelongsToUserId.HasValue)
            {
                result.Agent = await _userService.GetAsync(batch.BelongsToUserId.Value);
            }

            return result;
        }

        public async Task<BatchEntity> GetForPrintAsync(int id)
        {
            // View needs batchId, batch.Creator.CanadaPhoneNumber, batch.Creator.Code, batch.Recipient.Name, batch.Recipient.CanadaPhoneNumber, total Orders
            // Controller needs batch.RecipientAddressId
            var batch = await _context.Batches.Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                .Include(b => b.User).ThenInclude(u => u.Customer)
                .Include(b => b.RecipientUser).ThenInclude(u => u.Customer)
                .Select(b => new Batch
                {
                    Id = b.Id,
                    RecipientAddressId = b.RecipientAddressId,
                    BatchBoxes = b.BatchBoxes.Select(bx => new BatchBox
                    {
                        Number = bx.Number,
                        BatchBoxOrderMaps = bx.BatchBoxOrderMaps.Select(m => new BatchBoxOrderMap
                        {
                            Order = new TransportOrder { Id = m.OrderId }
                        }).ToList() 
                    }).ToList(),
                    User = new User
                    {
                        CanadaPhoneNumber = b.User.CanadaPhoneNumber,
                        OrderStartNumber = b.User.OrderStartNumber
                    },
                    RecipientUser = new User {
                        Customer = new Customer
                        {
                            Name = b.RecipientUser.Customer.Name
                        },
                        CanadaPhoneNumber = b.RecipientUser.CanadaPhoneNumber
                    }

                })
                .FirstAsync(b => b.Id == id);
            var result = _mapper.Map<BatchEntity>(batch);
            return result;
        }

        public async Task<BatchEntity> GetForEditAsync(int id)
        {
            // Need batchId, batch.GroupType, batch.RouteId, otherOrders, box.Number, count box.order, total box.order.WeightKg
            // batch.Route.Type, batch.Stage
            var batchBase = await _context.Batches.Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order)
                .Include(b => b.Route)
                .Include(b => b.LoadDeliveryBatches)
                .FirstAsync(b => b.Id == id);
            var batchWithOtherOrders = await _context.Batches.Include(b => b.BatchOtherOrders)
                .FirstAsync(b => b.Id == id);
            var batchWithMergedOrders = await _context.Batches.Include(b => b.BatchBoxMaps).ThenInclude(bx => bx.BatchBox).ThenInclude(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order)
                .FirstAsync(b => b.Id == id);
            batchBase.BatchOtherOrders = batchWithOtherOrders.BatchOtherOrders;
            batchBase.BatchOrderMaps = batchWithMergedOrders.BatchOrderMaps;
            foreach (var b in batchBase.BatchBoxes)
            {
                b.BatchBoxOrderMaps = b.BatchBoxOrderMaps.Where(bbom => bbom.Order.CompanyId == Config.COMPANY_ID).ToList();
            }
            var result = _mapper.Map<BatchEntity>(batchBase);
            return result;
        }

        public async Task<PalletBatchEntity> GetForEditPalletAsync(int id)
        {
            // Need batchId, batch.GroupType, batch.RouteId, otherOrders, box.Number, count box.order, total box.order.WeightKg
            // batch.Route.Type, batch.Stage
            var batch = await _context.Batches.Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                .ThenInclude(m => m.Order)
                .Include(b => b.BatchOtherOrders)
                .Include(b => b.BatchPallets)
                .Include(b => b.BatchBoxMaps).ThenInclude(bx => bx.BatchBox).ThenInclude(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order)
                .FirstAsync(b => b.Id == id && b.CompanyId == Config.COMPANY_ID);
            var result = _mapper.Map<PalletBatchEntity>(batch);
            return result;
        }
        public async Task UpdateBatchBox(int boxId, double? length, double? width, double? height, double? actualWeightKg)
        {
            var box = await _context.BatchBoxes.FirstOrDefaultAsync(b => b.Id == boxId);
            if (box == null)
            {
                throw new Exception("箱号 " + boxId + " 不存在");
            }
            box.Length = length;
            box.Width = width;
            box.Height = height;
            box.ActualWeightKg = actualWeightKg;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrdersLoadDeliveryProperties(int batchId)
        {
            var batch = await _context.Batches.Select(b => new Batch{Id = b.Id, GroupType = b.GroupType}).FirstOrDefaultAsync(b => b.Id == batchId && b.CompanyId == Config.COMPANY_ID);
            if (batch == null)
            {
                throw new Exception("批次号 " + batchId + " 不存在");
            }
            if ((BatchGroupType)batch.GroupType != BatchGroupType.LoadDelivery)
            {
                throw new Exception("批次 " + batchId + " 不是装车发货批次");
            }
            await _context.Database.ExecuteSqlRawAsync(
                @"
                UPDATE transport_order o
                JOIN batch_box_order_map bbom ON o.id=bbom.OrderId
                JOIN batch_box bb ON bb.Id=bbom.BatchBoxId
                JOIN batch b ON b.Id=bb.BatchId
                SET o.LoadDeliveryBatchId=bb.BatchId, o.LoadDeliveryBatchName=b.Name
                WHERE bb.BatchId=" + batchId
            );
        }
        public async Task<BatchEntity> GetByBoxIdAsync(int id)
        {
            var box = await _context.BatchBoxes.FirstAsync(b => b.Id == id);
            return await GetAsync(box.BatchId);
        }

        public async Task<BatchEntity> GetForAddOrderAsync(int boxId)
        {
            // Need batch.Id, all boxes' OrderId, all boxes' number, batch.GroupType, batch.WareHouseId,
            // batch.Route.Type, batch.Stage, batch.Name
            var box = await _context.BatchBoxes.FirstAsync(b => b.Id == boxId);
            var batch = await _context.Batches.Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order)
            .Include(b => b.Route)
            .Select(b => new Batch
            {
                Id = b.Id,
                GroupType = b.GroupType,
                WarehouseId = b.WarehouseId,
                Route = b.RouteId == null ? null : new Route { Type = b.Route.Type },
                Stage = b.Stage,
                Name = b.Name,
                BatchBoxes = b.BatchBoxes.Select(bx => new BatchBox
                {
                    Number = bx.Number,
                    BatchBoxOrderMaps = bx.BatchBoxOrderMaps.Select(m => new BatchBoxOrderMap
                    {
                        Order = new TransportOrder
                        {
                            Id = m.OrderId
                        }
                    }).ToList() 
                }).ToList()

            })
            .FirstOrDefaultAsync(b => b.Id == box.BatchId);
            return new BatchEntity
            {
                Id = batch.Id,
                GroupType = (BatchGroupType)batch.GroupType,
                WarehouseId = batch.WarehouseId,
                Route = batch.Route == null ? null : new RouteEntity { Type = (RouteType)batch.Route.Type },
                Stage = (BatchStageType)batch.Stage,
                Name = batch.Name,
                Boxes = batch.BatchBoxes.Select(bx => new BatchBoxEntity
                {
                    Number = bx.Number,
                    Orders = bx.BatchBoxOrderMaps.Select(m => new OrderEntity
                    {
                        Id = m.OrderId
                    })
                }).ToList()
            };
        }

        public async Task<BatchEntity> GetForEditBoxAsync(int boxId)
        {
            // Need batch.name, batch.id, batchbox.Id, batchbox.Number, 
            // order.Id, order.OrderNumber, order.DomesticNumber, order.ScanStatusType, order.PickupLocation.Name, order.WeightKg, order.Status
            // order.Creator.Name, order.Creator.CanadaPhoneNumber, order.Creator.BelongsTo.Name.
            var box = await _context.BatchBoxes.Include(bx => bx.Batch)
                .Include(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.BelongsToNavigation)
                .Include(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order).ThenInclude(o => o.PickUpLocation)
                .Include(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order).ThenInclude(o => o.OrderStatuses)
                .Include(bx => bx.BatchBoxOrderMaps).ThenInclude(m => m.Order).ThenInclude(o => o.OrderBaggages)
                .Select(bx => new BatchBox
                {
                    Id = bx.Id,
                    Number = bx.Number,
                    BatchId = bx.BatchId,
                    Batch = new Batch { Name = bx.Batch.Name },
                    BatchBoxOrderMaps = bx.BatchBoxOrderMaps.Select(m => new BatchBoxOrderMap()
                    {
                        Order = new TransportOrder
                        {
                            Id = m.OrderId,
                            OrderNumber = m.Order.OrderNumber,
                            DomesticNumber = m.Order.DomesticNumber,
                            PickUpLocation = new PickUpLocation { Name = m.Order.PickUpLocation.Name },
                            WeightKg = m.Order.WeightKg,
                            CreatedBy = new User {
                                Customer = new Customer {Name = m.Order.CreatedBy.Customer.Name },
                                CanadaPhoneNumber = m.Order.CreatedBy.CanadaPhoneNumber,
                                BelongsToNavigation = new User { Customer = new Customer { Name = m.Order.CreatedBy.BelongsToNavigation.Customer.Name } }
                            },
                            CompanyId = m.Order.CompanyId,
                            OrderStatuses = m.Order.OrderStatuses,
                            OrderBaggages = m.Order.OrderBaggages,
                        }
                    }).ToList()
              })
              .FirstAsync(b => b.Id == boxId);
            return new BatchEntity
            {
                Id = box.BatchId,
                Name = box.Batch.Name,
                Boxes = new List<BatchBoxEntity>
                {
                    new BatchBoxEntity
                    {
                        Id = box.Id,
                        Number = box.Number,
                        Orders = box.BatchBoxOrderMaps.Select(m => _mapper.Map<OrderEntity>(m.Order))
                    }
                }
            };
        }

        private void _validateAddOrder(BatchEntity batch, OrderEntity order)
        {
            var boxHasTheOrderAlready = batch.Boxes.FirstOrDefault(bx => bx.Orders.Any(o => o.Id == order.Id));
            if (boxHasTheOrderAlready != null)
            {
                throw new Exception($"该单已经在该批次-{boxHasTheOrderAlready.Number}箱内");
            }
            if (batch.GroupType == BatchGroupType.DailyScan)
            {
                // if (batch.WarehouseId != order.Route?.WarehouseId)
                // {
                //     throw new Exception($"该单不属于同一个仓库");
                // }

                if (order.State == OrderState.PendingReturn)
                {
                    throw new Exception($"该单属于待退运");
                }

                if (order.State == OrderState.Draft)
                {
                    throw new Exception($"该单属于未匹配");
                }
            }
            if (batch.GroupType == BatchGroupType.DailyReturn && !order.Status.Any(s => s.Status == OrderStatusType.RequestCancel))
            {
                throw new Exception("向每日退运添加的单缺少申请退运状态");
            }
        }

        public async Task<BatchEntity> AddOrderAsync(int boxId, int orderId, OrderEntity order = null)
        {
            var batch = await GetForAddOrderAsync(boxId);
            if (order == null)
            {
                order = await _orderService.GetAsync(orderId);
            }
            _validateAddOrder(batch, order);

            // TODO: need perf tune
            //if (batch.GroupType == BatchGroupType.LoadDelivery)
            //{
            //    var isOrderInDailyScan = await _context.Batches.AnyAsync(b =>
            //        b.GroupType == (int)BatchGroupType.DailyScan &&
            //        b.BatchBoxes.Any(bx => bx.BatchBoxOrderMaps.Any(m => m.OrderId == orderId)));

            //    if (!isOrderInDailyScan)
            //    {
            //        throw new Exception($"该单不属于" + BatchGroupType.DailyScan.GetDescription());
            //    }
            //}

            //if (batch.GroupType == BatchGroupType.ExitGarageScan)
            //{
            //    var isOrderInDailyScan = await _context.Batches.AnyAsync(b =>
            //        b.GroupType == (int)BatchGroupType.Package &&
            //        b.BatchBoxes.Any(bx => bx.BatchBoxOrderMaps.Any(m => m.OrderId == orderId)));

            //    if (!isOrderInDailyScan)
            //    {
            //        throw new Exception($"该单不属于" + BatchGroupType.Package.GetDescription());
            //    }
            //}

            var destBox = await _context.BatchBoxes.FirstAsync(bx => bx.Id == boxId);
            destBox.BatchBoxOrderMaps.Add(new BatchBoxOrderMap() {OrderId = orderId});

            OrderState orderState;
            if (batch.GetNextGroupType(order.Route) == BatchGroupType.PendingDispatch)
            {
                var userPendingDispatch = await _context.Batches.Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .FirstOrDefaultAsync(b => b.RouteId == order.RouteId && b.GroupType == (int) BatchGroupType.PendingDispatch && b.RecipientUserId == order.Creator.Id);
                
                if (userPendingDispatch == null)
                {
                    userPendingDispatch = await CreateAsync(new BatchEntity()
                    {
                        Name = $"{order.Creator.Code} {order.Route.Name}",
                        GroupType = BatchGroupType.PendingDispatch,
                        RouteId = order.RouteId,
                        RecipientId = order.Creator.Id,
                        AgentId = order.Creator.BelongsTo.Id
                    }, false, orderId);
                }
                else if (userPendingDispatch.BatchBoxes.All(b => b.BatchBoxOrderMaps.All(m => m.OrderId != orderId)))
                {
                    var firstBox = userPendingDispatch.BatchBoxes.First();
                    await AddOrderAsync(firstBox.Id, orderId, order);
                }

                orderState = _mapper.Map<BatchEntity>(userPendingDispatch).GetOrderState();
            }
            else
            {
                orderState = batch.GetOrderState();
            }

            var dbOrder = await _context.TransportOrders.FirstAsync(o => o.Id == order.Id);
            if (orderState != OrderState.None)
            {
                dbOrder.State = (int)orderState;
            }

            // 装车发货批次，加单时，不再更新，只有点击“更新批次名”按键时，才更新。
            // if (batch.GroupType == BatchGroupType.LoadDelivery)
            // {
            //     dbOrder.LoadDeliveryBatchName = batch.Name;
            //     dbOrder.LoadDeliveryBatchId = batch.Id;
            // }

            // TODO: add hard refresh
            //_memoryCache.Remove($"batch-{batch.Id}");
            await _context.SaveChangesAsync();
            if (batch.GroupType == BatchGroupType.Package)
            {
                // 装箱打包批次里的扫描，加 “装箱打包扫描” 的后台操作记录
                await _orderService.AddInternalStatus(OrderStatusType.PackagingScan, _session.CurrentUser.Id, order.Id);
            }
            else if (batch.GroupType == BatchGroupType.ExitGarageScan)
            {
                // 出库扫描批次里的扫描，加 “出库扫描” 的后台操作记录
                await _orderService.AddInternalStatus(OrderStatusType.LeavingWarehouseScan, _session.CurrentUser.Id, order.Id);
            }
            else if (batch.GroupType == BatchGroupType.LoadDelivery)
            {
                // 装车发货批次里的扫描，加 “装车发货扫描” 的后台操作记录
                await _orderService.AddInternalStatus(OrderStatusType.OnboardingScan, _session.CurrentUser.Id, order.Id);
            }
            else if (batch.GroupType == BatchGroupType.WarehouseReceive)
            {
                // 仓库收货批次里的扫描，加 “到货扫描” 的后台操作记录
                await _orderService.AddInternalStatus(OrderStatusType.ArrivalScan, _session.CurrentUser.Id, order.Id);
            }
            else if (batch.GroupType == BatchGroupType.DailyReturn)
            {
                if (order.Status.Any(s => s.Status == OrderStatusType.RequestCancel))
                {
                    // 每日退运批次里的扫描，加 “包裹退运” 的后台操作记录
                    await _orderService.AddInternalStatus(OrderStatusType.CancelPackage, _session.CurrentUser.Id, order.Id);
                }
            }
            else
            {
                // 其他批次里的扫描，批次默认是每日到货类型，加 “入库扫描” 的操作记录
                await _orderService.AddStatus(OrderStatusType.EnterWarehouseAndScan, _session.CurrentUser.Id, order);
                // 直邮线路扫描，不光加入入库扫描状态，还要加入等待用户发货状态
                if (order.Route.Type == RouteType.Direct)
                {
                    await _orderService.AddStatus(OrderStatusType.PendingCustomerDispatch, _session.CurrentUser.Id, order);
                }
            }

            return batch;
        }

        public async Task AddOtherOrderAsync(int boxId, string number)
        {
            var batchBox = await _context.BatchBoxes.Include(bx => bx.Batch).ThenInclude(b => b.BatchOtherOrders).FirstOrDefaultAsync(bx => bx.Id == boxId);
            if (batchBox != null && !batchBox.Batch.BatchOtherOrders.Select(b => b.OtherOrder).Contains(number))
            {
                batchBox.Batch.BatchOtherOrders.Add(new BatchOtherOrder()
                {
                    BatchId = batchBox.Batch.Id,
                    OtherOrder = number
                });

                // TODO: add hard refresh
                //_memoryCache.Remove($"batch-{batch.Id}");
                await _context.SaveChangesAsync();
            }
        }
        public async Task<WarehouseReceiveBatchEntity> SaveWarehouseReceiveAsync(WarehouseReceiveBatchEntity model)
        {
            if (model.Id == 0)
            {
                var result = await CreateWarehouseReceiveAsync(model);
                model.Id = result.Id;
                return model;
            }
            else
            {
                var result = await UpdateWarehouseReceiveAsync(model);
                return model;
            }
        }

        public async Task<LoadDeliveryBatchEntity> SaveLoadDeliveryAsync(LoadDeliveryBatchEntity model)
        {
            if (model.Id == 0)
            {
                var result = await CreateLoadDeliveryAsync(model);
                model.Id = result.Id;
                return model;
            }
            else
            {
                var result = await UpdateLoadDeliveryAsync(model);
                return model;
            }
        }

        public async Task<PalletBatchEntity> SavePalletAsync(PalletBatchEntity model)
        {
            if (model.Id == 0)
            {
                var result = await CreatePalletAsync(model);
                model.Id = result.Id;
                return model;
            }
            else
            {
                var result = await UpdatePalletAsync(model);
                return model;
            }
        }

        public async Task<BatchEntity> SaveAsync(BatchEntity model)
        {
            try
            {
                if (model.Id == 0)
                {
                    Console.WriteLine(nameof(SaveAsync) + " start ");
                    var result = await CreateAsync(model);
                    model.Id = result.Id;
                    Console.WriteLine(nameof(SaveAsync) + " end ");
                    return model;
                }
                else
                {
                    var result = await UpdateAsync(model);
                    return model;
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task AddBoxAsync(int id, int boxNumber)
        {
            var batch = await _context.Batches.Include(b => b.BatchBoxes).FirstAsync(b => b.Id == id);

            if (batch.BatchBoxes.Any(bx => bx.Number == boxNumber))
            {
                throw new ArgumentException($"This box number {boxNumber} already exists in batch {id}");
            }

            var batchBox = new BatchBox() { Number = boxNumber, Name = $"{id} - {boxNumber}" };
            batch.BatchBoxes.Add(batchBox);

            _memoryCache.Remove($"batch-{batch.Id}");
            await _context.SaveChangesAsync();
            batch.BatchBoxMaps = new List<BatchBoxMap>();
            batch.BatchBoxMaps.Add(new BatchBoxMap { BatchId = id, BoxId = batchBox.Id });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOrderAsync(int boxId, int orderId)
        {
            var box = await _context.BatchBoxOrderMaps.Include(b => b.BatchBox)
                .FirstOrDefaultAsync(b => b.BatchBoxId == boxId && b.OrderId == orderId);
            
            if (box == null)
            {
                return;
            }

            _context.BatchBoxOrderMaps.Remove(box);

            // TODO: add hard refresh
            //_memoryCache.Remove($"batch-{box.BatchBox.BatchId}");
            await _context.SaveChangesAsync();
        }

        public async Task CreateDailyBatchPerWarehouseAsync(BatchGroupType groupType)
        {
            var today = _date.UserNow.Date;
            var existingBatches = await _context.Batches.Where(b =>
                b.IsFromChina && b.DateCreated >= today && b.GroupType == (int) groupType).ToListAsync();

            
            if (!existingBatches.Any())
            {
                await CreateAsync(new BatchEntity()
                {
                    Name = $"{today:yyyy-MM-dd} {groupType.GetDescription()}",
                    GroupType = groupType,
                });
            }

        }

        public async Task MoveNextAsync(int id)
        {
            var batch = await GetAsyncForMoveNext(id);
            if (batch.Route == null)
            {
                throw new Exception($"Batch {batch.Name} has no Route.");
            }
            var originalType = batch.GroupType;

            batch.GroupType = batch.GetNextGroupType();

            await SaveAsync(batch);

            await UpdateOrderStateAsync(batch);

            // 对于直邮线的出库扫描批次，点 "下一步" 中，如果里面的单有 "客户确认" 状态，那么一律添加 "已发货"
            if (batch.Route.Type == RouteType.Direct && originalType == BatchGroupType.ExitGarageScan)
            {
                var orders = batch.Boxes.SelectMany(b => b.Orders).Where(o => 
                    o.Status.Any(s => s.Status == OrderStatusType.CustomerConfirm) &&
                    !o.Status.Any(s => s.Status == OrderStatusType.Dispatched)
                ).ToArray();
                await _orderService.AddStatus(OrderStatusType.Dispatched, _session.CurrentUser.Id, orders);
            }

            // 装箱打包批次点击 "下一步"，批次里的单添加 "等待确认运费" 状态
            if (originalType == BatchGroupType.Package)
            {
                var orders = batch.Boxes.SelectMany(b => b.Orders).ToArray();
                await _orderService.AddStatus(OrderStatusType.PendingConfirmationMoney, _session.CurrentUser.Id, orders);
            }

            // 装车发货批次点击 "下一步"，批次里的单添加 "已完成" 状态
            if (originalType == BatchGroupType.LoadDelivery)
            {
                var orders = batch.Boxes.SelectMany(b => b.Orders).ToArray();
                await _orderService.AddStatus(OrderStatusType.Completed, _session.CurrentUser.Id, orders);
            }

            if (originalType == BatchGroupType.Package && batch.RecipientId.HasValue)
            {
                #pragma warning disable 4014
                Task.Run(async () =>
                {
                    var message = MessageUtils.GetParcelDispatchNewMessage(batch.Name, batch.WeightKg, batch.TotalExpense);
                    var userId = _session.CurrentUser.Id;
                    var smsUserInfo = await _smsService.GetSmsUserInfoByUserIdAsync(batch.RecipientId.Value);
                    bool success = await _smsService.SendAsync(new SmsRequest[]
                    {
                        new SmsRequest
                        {
                            Message = message,
                            Level = smsUserInfo.Level,
                            MobilePhoneNumber = smsUserInfo.MobilePhoneNumber,
                            OrderStartNumber = smsUserInfo.OrderStartNumber,
                            BelongsTo = smsUserInfo.BelongsToName,
                            FullName = smsUserInfo.FullName
                        }
                    }, userId);
                    if (success)
                    {
                        
                    }

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
                            MessageUtils.ParcelDispatchEmailSubject,
                            MessageUtils.GetParcelDispatchEmailBody(batch.Name, batch.WeightKg, batch.TotalExpense)
                        );
                    }
                })
                .ConfigureAwait(false);
                #pragma warning restore 4014
            }
        }

        private async Task UpdateOrderStateAsync(BatchEntity batch)
        {
            var orderState = batch.GetOrderState();
            if (orderState == OrderState.None)
            {
                return;
            }

            foreach (var order in batch.Boxes.SelectMany(b => b.Orders))
            {
                var orderToUpdate = await _context.TransportOrders.FirstAsync(o => o.Id == order.Id && o.CompanyId == Config.COMPANY_ID);
                orderToUpdate.State = (int)orderState;
            }

            await _context.SaveChangesAsync();
        }

        public async Task PayAndMoveNextAsync(int id, PayType payType)
        {
            var batch = await GetForPayAsync(id);
            UserEntity deductFromUser = null;
            if (batch.GroupType == BatchGroupType.WarehouseCost)
            {
                deductFromUser = batch.Agent;
            }
            else if (batch.GroupType == BatchGroupType.PickUpLocation)
            {
                deductFromUser = batch.PickUpLocation.Owner;
            }
            else
            {
                deductFromUser = batch.Recipient;
            }
            var transactionType = batch.GroupType == BatchGroupType.WarehouseCost
                ? TransactionType.WarehouseCost
                : TransactionType.BatchDeduct;

            if (deductFromUser.Balance + 1 < batch.TotalExpense)
            {
                var error = $"User's balance {deductFromUser.Balance} is less than the cost {batch.TotalExpense}";
                throw new Exception(error);
            }

            _userService.Transfer(deductFromUser.Id, _session.CurrentUser.Id, batch.TotalExpense, payType, transactionType, id);
            await MoveNextAsync(batch.Id);

            if (batch.Route.Type == RouteType.Direct && batch.GetOrderState() == OrderState.Done)
            {
                await _orderService.AddStatus(OrderStatusType.Paid, _session.CurrentUser.Id,
                    batch.Boxes.SelectMany(b => b.Orders).ToArray());
            }
        }

        public async Task SplitByRecipientsAsync(int id)
        {
            var batch = await GetAsyncForSplitByRecipients(id);
            if (batch.GroupType != BatchGroupType.WarehouseReceive && batch.GroupType != BatchGroupType.PickUpLocation)
            {
                throw new Exception($"批次不是仓库收货或取货点，不能拆分。");
            }
            if (!batch.GetActionTypes().Contains(BatchActionType.SplitByRecipients))
            {
                throw new Exception(
                    $"Cannot split this batch. Id: {id}, group type: {batch.GroupType.GetDescription()}, action types: {string.Join(",", batch.GetActionTypes())}.");
            }
            var ordersToSplit = batch.Boxes.SelectMany(bx => bx.Orders).Distinct().ToList();

            // split by recipients
            await SplitByCriteria(ordersToSplit, batch, o => o.Creator, BatchGroupType.PendingPickUp, true, (u, orders) => new BatchEntity()
            {
                Name = $"{batch.Name} {u.Code} {u.Name} {BatchGroupType.PendingPickUp.GetDescription()}",
                RouteId = batch.RouteId,
                MasterBatchId = batch.MasterBatchId,
                GroupType = BatchGroupType.PendingPickUp,
                RecipientId = u.Id,
                AgentId = u.BelongsTo?.Id,
            });
            await _context.SaveChangesAsync();
        }

        public async Task SplitByAgentsAsync(int id)
        {
            var batch = await GetAsyncForSplitByAgents(id);
            if (batch.GroupType != BatchGroupType.LoadDelivery)
            {
                throw new Exception($"批次不是装车发货，不能拆分。");
            }
            if (!batch.GetActionTypes().Contains(BatchActionType.SplitByAgents))
            {
                throw new Exception(
                    $"Cannot split this batch. Id: {id}, group type: {batch.GroupType.GetDescription()}, action types: {string.Join(",", batch.GetActionTypes())}.");
            }
            var ordersToSplit = batch.Boxes.SelectMany(bx => bx.Orders).Distinct().ToList();
            var creators = ordersToSplit.Select(o => o.Creator).Distinct();
            var noAgent = creators.Where(c => c.BelongsToId == null);
            if (noAgent.Any())
            {
                throw new Exception($"用户: \n{string.Join(", \n", noAgent.Select(u => u.OrderStartNumber))} \n没有群主。");
            }
            // split by agents
            await SplitByCriteria(ordersToSplit, batch, o => o.Creator.BelongsTo, BatchGroupType.AgentCommission, true, (u, orders) => new BatchEntity()
            {
                Name = $"{batch.Name} {u.Code} {u.Name} {BatchGroupType.AgentCommission.GetDescription()}",
                RouteId = batch.RouteId,
                MasterBatchId = batch.MasterBatchId,
                GroupType = BatchGroupType.AgentCommission,
                AgentId = u.BelongsTo?.Id,
            });
            await _context.SaveChangesAsync();
        }

        public async Task SplitByLocationsAsync(int id)
        {
            var batch = await GetAsyncForSplitByLocations(id);
            if (batch.GroupType != BatchGroupType.LoadDelivery && batch.GroupType != BatchGroupType.WarehouseReceive)
            {
                throw new Exception($"批次不是装车发货或仓库收货，不能拆分。");
            }
            if (!batch.GetActionTypes().Contains(BatchActionType.SplitByLocations))
            {
                throw new Exception(
                    $"Cannot split this batch. Id: {id}, group type: {batch.GroupType.GetDescription()}, action types: {string.Join(",", batch.GetActionTypes())}.");
            }
            var ordersToSplit = batch.Boxes.SelectMany(bx => bx.Orders).Distinct().ToList();
            var noPickUp = ordersToSplit.Where(o => o.PickUpLocationId == null);
            if (noPickUp.Any())
            {
                throw new Exception($"运单: \n{string.Join(", \n", noPickUp.Select(o => o.OrderNumber))} \n没有取货点。");
            }
            // split by pick up locations => 取货点扣款
            await SplitByLocationCriteria(ordersToSplit, batch, o => o.PickUpLocation.Id, BatchGroupType.PickUpLocation, false, (u, orders) => 
            {
                var entity = new BatchEntity()
                {
                    GroupType = BatchGroupType.PickUpLocation
                };

                var pickUpLocationName = "";
                
                var orderEntity = orders.FirstOrDefault();
                if (orderEntity == null)
                {
                    pickUpLocationName = "没有运单";
                }
                else
                {
                    pickUpLocationName = orderEntity.PickUpLocation?.Name ?? $"运单{orderEntity.Id}没有取货点";
                }
                entity.Name = $"{batch.Name} {pickUpLocationName} {BatchGroupType.PickUpLocation.GetDescription()}";
                entity.RouteId = batch.RouteId;
                entity.MasterBatchId  = batch.MasterBatchId;
                entity.PickUpLocationId = orders.FirstOrDefault(o => o.PickUpLocationId != null)?.PickUpLocationId;
                return entity;
            });
            await _context.SaveChangesAsync();
        }

        public async Task SplitByNonAgent(int id)
        {
            var batch = await GetAsyncForSplitByNonLocation(id);
            if (batch.GroupType != BatchGroupType.LoadDelivery && batch.GroupType != BatchGroupType.WarehouseReceive)
            {
                throw new Exception($"批次不是装车发货或仓库收货，不能拆分。");
            }
            if (!batch.GetActionTypes().Contains(BatchActionType.SplitByNonAgent))
            {
                throw new Exception(
                    $"Cannot split this batch. Id: {id}, group type: {batch.GroupType.GetDescription()}, action types: {string.Join(",", batch.GetActionTypes())}.");
            }
            var ordersToSplit = batch.Boxes.SelectMany(bx => bx.Orders).Distinct().ToList();
            
            var noPickUp = ordersToSplit.Where(o => o.PickUpLocationId == null);
            if (noPickUp.Any())
            {
                throw new Exception($"运单: \n{string.Join(", \n", noPickUp.Select(o => o.OrderNumber))} \n没有取货点。");
            }
            var creators = ordersToSplit.Select(o => o.Creator);
            var noAgent = creators.Where(c => c.BelongsTo == null);
            if (noAgent.Any())
            {
                throw new Exception($"运单的创建用户: \n{string.Join(", \n", noAgent.Select(o => o.OrderStartNumber))} \n没有群主。");
            }

            var agentIds = ordersToSplit.Select(o => o.Creator.BelongsToId).ToList();
            var locations = await _context.PickUpLocations
                .Where(p => p.BelongsToId.HasValue && agentIds.Contains(p.BelongsToId.Value)).ToListAsync();
            var locationMap = new Dictionary<int, long>();
            foreach (var pickUpLocation in locations)
            {
                locationMap[pickUpLocation.BelongsToId.Value] = pickUpLocation.Id;
            }

            var agents = await _context.PickUpLocations
                .Where(p => p.BelongsToId.HasValue && agentIds.Contains(p.BelongsToId.Value)).ToListAsync();

            // split by pick up location that not from agents
            var ordersThatPickUpNotAtAgent = ordersToSplit.Where(o => !locationMap.ContainsKey(o.Creator.BelongsToId.Value) || o.PickUpLocationId != locationMap[o.Creator.BelongsToId.Value]);
            await SplitByCriteria(ordersThatPickUpNotAtAgent, batch, o => o.Creator.BelongsTo, BatchGroupType.WarehouseCost, false, (u, orders) => new BatchEntity()
            {
                Name = $"{batch.Name} {u.Code} {u.Name} {BatchGroupType.WarehouseCost.GetDescription()}",
                RouteId = batch.RouteId,
                MasterBatchId = batch.MasterBatchId,
                GroupType = BatchGroupType.WarehouseCost,
                AgentId = u.Id,
            });
            await _context.SaveChangesAsync();
        }

        public async Task SplitByNonLocation(int id)
        {
            var batch = await GetAsyncForSplitByNonLocation(id);
            if (batch.GroupType != BatchGroupType.LoadDelivery && batch.GroupType != BatchGroupType.WarehouseReceive)
            {
                throw new Exception($"批次不是装车发货或仓库收货，不能拆分。");
            }
            if (!batch.GetActionTypes().Contains(BatchActionType.SplitByNonLocation))
            {
                throw new Exception(
                    $"Cannot split this batch. Id: {id}, group type: {batch.GroupType.GetDescription()}, action types: {string.Join(",", batch.GetActionTypes())}.");
            }
            var ordersToSplit = batch.Boxes.SelectMany(bx => bx.Orders).Distinct().ToList();
            var noPickUp = ordersToSplit.Where(o => o.PickUpLocationId == null);
            if (noPickUp.Any())
            {
                throw new Exception($"运单: \n{string.Join(", \n", noPickUp.Select(o => o.OrderNumber))} \n没有取货点。");
            }
            var creators = ordersToSplit.Select(o => o.Creator);
            var noAgent = creators.Where(c => c.BelongsTo == null);
            if (noAgent.Any())
            {
                throw new Exception($"运单的创建用户: \n{string.Join(", \n", noAgent.Select(o => o.OrderStartNumber))} \n没有群主。");
            }
            var agentIds = ordersToSplit.Select(o => o.Creator.BelongsToId).ToList();
            var locations = await _context.PickUpLocations
                .Where(p => p.BelongsToId.HasValue && agentIds.Contains(p.BelongsToId.Value)).ToListAsync();
            var locationMap = new Dictionary<int, long>();
            foreach (var pickUpLocation in locations)
            {
                locationMap[pickUpLocation.BelongsToId.Value] = pickUpLocation.Id;
            }

            // split by pick up location that not from agents
            var ordersThatPickUpNotAtAgent = ordersToSplit.Where(o => !locationMap.ContainsKey(o.Creator.BelongsToId.Value) || o.PickUpLocationId != locationMap[o.Creator.BelongsToId.Value]);
            await SplitByLocationCriteria(ordersThatPickUpNotAtAgent, batch, o => o.PickUpLocationId.Value, BatchGroupType.Bill, false, (l, orders) => new BatchEntity()
            {
                Name = $"{batch.Name} {l.Name} {BatchGroupType.Bill.GetDescription()}",
                RouteId = batch.RouteId,
                MasterBatchId = batch.MasterBatchId,
                GroupType = BatchGroupType.Bill,
                PickUpLocationId = l.Id,
            });
            await _context.SaveChangesAsync();
        }

        public async Task SplitAsync(int id)
        {
            var batch = await GetAsyncForSplit(id);

            /*
            if (!batch.MasterBatchId.HasValue && batch.GroupType != BatchGroupType.LoadDelivery)
            {
                throw new Exception(
                    $"没有装车发货 不能拆分. Id: {batch.Id}, group type: {batch.GroupType.GetDescription()}.");
            }
            */

            if (!batch.GetActionTypes().Contains(BatchActionType.Split))
            {
                throw new Exception(
                    $"Cannot split this batch. Id: {batch.Id}, group type: {batch.GroupType.GetDescription()}.");
            }

            var ordersToSplit = batch.Boxes.SelectMany(bx => bx.Orders).Distinct().ToList();

            if (batch.GroupType == BatchGroupType.LoadDelivery)
            {
                var agentIds = ordersToSplit.Select(o => o.Creator.BelongsToId).ToList();
                var locations = await _context.PickUpLocations
                    .Where(p => p.BelongsToId.HasValue && agentIds.Contains(p.BelongsToId.Value)).ToListAsync();
                var locationMap = new Dictionary<int, long>();
                foreach (var pickUpLocation in locations)
                {
                    locationMap[pickUpLocation.BelongsToId.Value] = pickUpLocation.Id;
                }

                // split by pick up location that not from agents
                var ordersThatPickUpNotAtAgent = ordersToSplit.Where(o => !locationMap.ContainsKey(o.Creator.BelongsToId.Value) || o.PickUpLocationId != locationMap[o.Creator.BelongsToId.Value]);
                await SplitByCriteria(ordersThatPickUpNotAtAgent, batch, o => o.Creator.BelongsTo, BatchGroupType.WarehouseCost, false, (u, orders) => new BatchEntity()
                {
                    Name = $"{batch.Name} {u.Code} {u.Name} {BatchGroupType.WarehouseCost.GetDescription()}",
                    RouteId = batch.RouteId,
                    MasterBatchId = batch.MasterBatchId,
                    GroupType = BatchGroupType.WarehouseCost,
                    AgentId = u.Id,
                });
            }
            else
            {
                // split by recipients
                await SplitByCriteria(ordersToSplit, batch, o => o.Creator, BatchGroupType.PendingPickUp, true, (u, orders) => new BatchEntity()
                {
                    Name = $"{batch.Name} {u.Code} {u.Name} {BatchGroupType.PendingPickUp.GetDescription()}",
                    RouteId = batch.RouteId,
                    MasterBatchId = batch.MasterBatchId,
                    GroupType = BatchGroupType.PendingPickUp,
                    RecipientId = u.Id,
                    AgentId = u.BelongsTo?.Id,
                });
            }
            
            await _context.SaveChangesAsync();
        }

        private async Task SplitByLocationCriteria(IEnumerable<OrderEntity> ordersToSplit, BatchEntity batch, Func<OrderEntity, int> groupByCriteria, BatchGroupType groupTypeToSearch, bool searchRecipient, Func<PickUpLocationEntity, IEnumerable<OrderEntity>, BatchEntity> createBatchFunc)
        {
            var locations = ordersToSplit.GroupBy(groupByCriteria);
            var masterBatchId = batch.GroupType == BatchGroupType.LoadDelivery ? batch.Id : batch.MasterBatchId;
            foreach (var location in locations)
            {
                var locationOrders = location.ToList();
                var existingBatches = _context.Batches.Include(b => b.BatchBoxes)
                    .ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .Where(b =>
                        b.MasterBatchId == masterBatchId && b.GroupType == (int)groupTypeToSearch);

                if (searchRecipient)
                {
                    existingBatches = existingBatches.Where(b => b.RecipientUserId == location.Key);
                }
                else
                {
                    existingBatches = existingBatches.Where(b => b.BelongsToUserId == location.Key);
                }

                var existingBatch = await existingBatches.FirstOrDefaultAsync();
                if (existingBatch != null)
                {
                    var existingOrderIds = existingBatch.BatchBoxes.SelectMany(o => o.BatchBoxOrderMaps)
                        .Select(o => o.OrderId).ToList();
                    var orderState = batch.GetOrderState(BatchGroupType.PendingPickUp);

                    foreach (var order in locationOrders)
                    {
                        if (existingOrderIds.Contains(order.Id))
                        {
                            continue;
                        }

                        // add to box
                        var firstBatchBox = existingBatch.BatchBoxes.First();
                        if (firstBatchBox.BatchBoxOrderMaps.Any(o => o.OrderId == order.Id))
                        {
                            Console.WriteLine("Batch: " + existingBatch.Id + ", " + existingBatch.Name + ", box: " + firstBatchBox.Id + ", order: " + order.Id + " already exists.");
                        }
                        else
                        {
                            firstBatchBox.BatchBoxOrderMaps.Add(new BatchBoxOrderMap()
                            {
                                OrderId = order.Id
                            });
                        }

                        // set order state
                        if (orderState != OrderState.None)
                        {
                            var orderToUpdate = await _context.TransportOrders.FirstAsync(o => o.Id == order.Id);
                            orderToUpdate.State = (int)orderState;
                        }
                    }
                }
                else
                {
                    var locationBatch = createBatchFunc(locationOrders.First().PickUpLocation, locationOrders);
                    await CreateAsync(locationBatch, false, locationOrders.Select(o => o.Id).ToArray());
                }
            }
        }

        private async Task SplitByCriteria(IEnumerable<OrderEntity> ordersToSplit, BatchEntity batch, Func<OrderEntity, UserEntity> groupByCriteria, BatchGroupType groupTypeToSearch, bool searchRecipient, Func<UserEntity, IEnumerable<OrderEntity>, BatchEntity> createBatchFunc)
        {
            var agents = ordersToSplit.GroupBy(groupByCriteria);
            var masterBatchId = batch.GroupType == BatchGroupType.LoadDelivery ? batch.Id : batch.MasterBatchId;
            foreach (var agent in agents)
            {
                var recipientOrders = agent.ToList();
                var existingBatches = _context.Batches.Include(b => b.BatchBoxes)
                    .ThenInclude(bx => bx.BatchBoxOrderMaps)
                    .Where(b =>
                        b.MasterBatchId == masterBatchId && b.GroupType == (int)groupTypeToSearch);

                if (searchRecipient)
                {
                    existingBatches = existingBatches.Where(b => b.RecipientUserId == agent.Key.Id);
                }
                else
                {
                    existingBatches = existingBatches.Where(b => b.BelongsToUserId == agent.Key.Id);
                }

                var existingBatch = await existingBatches.FirstOrDefaultAsync();
                if (existingBatch != null)
                {
                    var existingOrderIds = existingBatch.BatchBoxes.SelectMany(o => o.BatchBoxOrderMaps)
                        .Select(o => o.OrderId).ToList();
                    var orderState = batch.GetOrderState(BatchGroupType.PendingPickUp);

                    foreach (var order in recipientOrders)
                    {
                        if (existingOrderIds.Contains(order.Id))
                        {
                            continue;
                        }

                        // add to box
                        var firstBatchBox = existingBatch.BatchBoxes.First();
                        if (firstBatchBox.BatchBoxOrderMaps.Any(o => o.OrderId == order.Id))
                        {
                            Console.WriteLine("Batch: " + existingBatch.Id + ", " + existingBatch.Name + ", box: " + firstBatchBox.Id + ", order: " + order.Id + " already exists.");
                        }
                        else
                        {
                            firstBatchBox.BatchBoxOrderMaps.Add(new BatchBoxOrderMap()
                            {
                                OrderId = order.Id
                            });
                        }

                        // set order state
                        if (orderState != OrderState.None)
                        {
                            var orderToUpdate = await _context.TransportOrders.FirstAsync(o => o.Id == order.Id);
                            orderToUpdate.State = (int) orderState;
                        }
                    }
                }
                else
                {
                    var recipientBatch = createBatchFunc(agent.Key, recipientOrders);
                    await CreateAsync(recipientBatch, false, recipientOrders.Select(o => o.Id).ToArray());
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var batch = await _context.Batches.FirstAsync(b => b.Id == id);

            var relatedBatches = await _context.Batches.Where(b => b.MasterBatchId == batch.Id).ToListAsync();
            _context.Batches.RemoveRange(relatedBatches);

            var boxes = await _context.BatchBoxes.Where(bx => bx.BatchId == id).ToListAsync();
            var boxIds = boxes.Select(bx => bx.Id).ToList();
            var maps = await _context.BatchBoxOrderMaps.Where(m => boxIds.Contains(m.BatchBoxId)).ToListAsync();

            _context.BatchBoxOrderMaps.RemoveRange(maps);
            _context.BatchBoxes.RemoveRange(boxes);
            _context.Batches.Remove(batch);

            await _context.SaveChangesAsync();
        }

        public async Task MergeAsync(int targetBatchId, int sourceBatchId, int? sourceBoxNumber)
        {
            var targetBatch = await GetAsyncForMerge(targetBatchId);
            var sourceBatch = await GetAsyncForMerge(sourceBatchId);

            if (sourceBoxNumber.HasValue && sourceBatch.Boxes.All(b => b.Number != sourceBoxNumber.Value))
            {
                throw new Exception($"源箱号不存在: {sourceBoxNumber}");
            }

            var orderIdsToMerge = sourceBoxNumber.HasValue
                ? sourceBatch.Boxes.First(b => b.Number == sourceBoxNumber.Value).Orders.Select(o => o.Id)
                : sourceBatch.Boxes.SelectMany(o => o.Orders).Select(o => o.Id);

            orderIdsToMerge = orderIdsToMerge.Distinct();

            // TODO: add status
            var targetBoxNumber = targetBatch.Boxes.Max(b => b.Number) + 1;
            var targetBox = new BatchBox() {BatchId = targetBatchId, Number = targetBoxNumber};
            await _context.BatchBoxes.AddAsync(targetBox);
            await _context.SaveChangesAsync();

            var targetOrderIds = targetBatch.Boxes.SelectMany(b => b.Orders).Select(o => o.Id).ToList();
            var destBox = await _context.BatchBoxes.FirstAsync(bx => bx.Id == targetBox.Id );

            foreach (var orderId in orderIdsToMerge)
            {
                if (targetOrderIds.Contains(orderId))
                {
                    continue;
                }

                await AddOrderAsync(destBox.Id, orderId);
            }
        }

        public async Task AcceptBoxAsync(int targetBatchId, int sourceBatchId, int? sourceBoxNumber)
        {
            var targetBatch = await GetAsyncForMerge(targetBatchId);
            var sourceBatch = await GetAsyncForMerge(sourceBatchId);
            var box = sourceBatch.Boxes.FirstOrDefault(b => b.Number == sourceBoxNumber.Value);
            if (box == null)
            {
                throw new Exception($"源箱号不存在: {sourceBoxNumber}");
            }
            var existingMap =  await _context.BatchBoxMaps.FirstOrDefaultAsync(m => m.BatchId == targetBatchId && m.BoxId == box.Id);
            if (existingMap == null)
            {
                await _context.BatchBoxMaps.AddAsync(new BatchBoxMap { BatchId = targetBatchId, BoxId = box.Id });
            }
            await _context.SaveChangesAsync();
        }


        public async Task CommissionAsync(int id)
        {
            var batch = await GetAsync(id);

            if (_session.CurrentUser.Balance < batch.Commission - 1)
            {
                throw new Exception($"Agent's balance {batch.Recipient.Balance} is less than the commission {batch.Commission}");
            }

            _userService.Transfer( _session.CurrentUser.Id, batch.Agent.Id, batch.Commission, PayType.Balance, TransactionType.ShippingCommission, id);
            await MoveNextAsync(batch.Id);

            if (batch.Route.Type == RouteType.Direct && batch.GetOrderState() == OrderState.Done)
            {
                await _orderService.AddStatus(OrderStatusType.Paid, _session.CurrentUser.Id,
                    batch.Boxes.SelectMany(b => b.Orders).ToArray());
            }
            if (batch.GroupType == BatchGroupType.LoadDelivery && batch.RecipientId.HasValue)
            {
                if (batch.RecipientAddressId == null)
                {
                    var message = MessageUtils.GetLoadDeliveryNewMessage(batch.IntNumber, batch.IntCarrier, "[Unknown]", batch.Recipient.Name, batch.Recipient.CanadaPhoneNumber);
                    await _logService.SaveSMSLog(batch.Id, _session.CurrentUser.Id, "批次 " + batch.Name + " 缺少收件人地址。", "", message);
                    return;
                }
                #pragma warning disable 4014
                Task.Run(async () =>
                {
                    var userId = _session.CurrentUser.Id;
                    var smsUserInfo = await _smsService.GetSmsUserInfoByUserIdAsync(batch.RecipientId.Value);
                    var address = await _userService.GetShippingAddressAsync(batch.RecipientAddressId.Value);
                    var message = MessageUtils.GetLoadDeliveryNewMessage(batch.IntNumber, batch.IntCarrier, address, batch.Recipient.Name, batch.Recipient.CanadaPhoneNumber);
                    await _smsService.SendAsync(new SmsRequest[]
                    {
                        new SmsRequest
                        {
                            Message = message,
                            MobilePhoneNumber = smsUserInfo.MobilePhoneNumber,
                            OrderStartNumber = smsUserInfo.OrderStartNumber,
                            BelongsTo = smsUserInfo.BelongsToName,
                            FullName = smsUserInfo.FullName,
                            Level = smsUserInfo.Level
                        }
                    }, userId);                
                })
                .ConfigureAwait(false);
                #pragma warning restore 4014
            }
        }

        public void RemoveCache(int id)
        {
            _memoryCache.Remove($"batch-{id}");
        }

        private async Task<Batch> UpdateAsync(BatchEntity model)
        {
            Batch batch;
            if (model.GroupType == BatchGroupType.LoadDelivery)
            {
                batch = await _context.Batches.Include(b => b.LoadDeliveryBatches).FirstAsync(b => b.Id == model.Id);
            }
            else
            {
                batch = await _context.Batches.FirstAsync(b => b.Id == model.Id);
            }
            var isIntNumberChanged = model.IntNumber != batch.IntNumber;
            var isIntCarrierChanged = model.IntCarrier != batch.IntCarrier;
            var originalStage = batch.Stage;

            batch.Name = model.Name;
            batch.IntNumber = model.IntNumber;
            batch.IntCarrier = model.IntCarrier;
            batch.Cost = model.Cost;
            batch.AddOnCost = model.AddOnCost;
            batch.StorageCost = model.StorageCost;
            batch.Duty = model.Duty;
            batch.Discount = model.Discount;
            batch.InsuranceFee = model.InsuranceFee;
            batch.HeBaoCost = model.HeBaoCost;
            batch.TotalExpense = model.TotalExpense;
            batch.PaidWeightKg = model.WeightKg;
            batch.RecipientUserId = model.RecipientId;
            batch.BelongsToUserId = model.AgentId;
            batch.PickUpLocationId = model.PickUpLocationId;
            batch.MasterBatchId = model.MasterBatchId;
            batch.WarehouseId = model.WarehouseId;
            batch.GroupType = (int) model.GroupType;
            batch.Stage = (int) model.Stage;
            batch.TargetWeightKg = model.TargetWeightKg;
            batch.Commission = model.Commission;
            batch.DateEntered = model.DateEntered;
            batch.Note = model.Note;

            List<TransportOrder> orders = null;
            if (isIntNumberChanged || isIntCarrierChanged)
            {
                orders = await _context.TransportOrders
                    .Where(o => o.BatchBoxOrderMaps.Any(box => box.BatchBox.BatchId == batch.Id)).ToListAsync();
                foreach (var order in orders)
                {
                    if (isIntNumberChanged)
                    {
                        order.SecondTrackNumber = batch.IntNumber;
                    }

                    if (isIntCarrierChanged)
                    {
                        order.SecondCarrier = batch.IntCarrier;
                    }
                }
            }

            if (model.GroupType == BatchGroupType.LoadDelivery)
            {
                batch.LoadDeliveryBatches = new List<LoadDeliveryBatch>
                {
                    new LoadDeliveryBatch
                    {
                        Id = batch.Id,
                        FlightInfo = model.FlightInfo,
                        CargoNumber = model.CargoNumber,
                        ArrivalTime = model.ArrivalTime,
                        WarehouseId = batch.LoadDeliveryBatches.First().WarehouseId,
                    }
                };
            }
            
            _memoryCache.Remove($"batch-{batch.Id}");
            await _context.SaveChangesAsync();

            if (batch.GroupType == (int)BatchGroupType.LoadDelivery)
            {
                if ((BatchStageType)originalStage != model.Stage)
                {
                    if (STAGE_TO_STATUS.TryGetValue(model.Stage, out OrderStatusType status))
                    {
                        orders = await _context.TransportOrders
                            .Where(o => o.BatchBoxOrderMaps.Any(box => box.BatchBox.BatchId == batch.Id)).ToListAsync();

                        foreach (var order in orders)
                        {
                            if (model.Stage == BatchStageType.Gathering) order.State = (int)OrderState.Gathering;
                            if (model.Stage == BatchStageType.LoadDelivery) order.State = (int)OrderState.LoadDelivery;
                            if (model.Stage == BatchStageType.Sailing) order.State = (int)OrderState.Sailing;
                            if (model.Stage == BatchStageType.Clearing) order.State = (int)OrderState.Clearing;
                            if (model.Stage == BatchStageType.Sorting) order.State = (int)OrderState.Sorting;
                        }


                        await _orderService.AddStatus(status, _session.CurrentUser.Id, orders.Select(o => new OrderEntity { Id = o.Id }).ToArray());
                    }
                }
            }

            return batch;
        }

        private async Task<Batch> UpdateWarehouseReceiveAsync(WarehouseReceiveBatchEntity model, bool isSaving = true, params int[] orderIds)
        {
            Batch batch = await _context.Batches.Include(b => b.BatchWarehouseReceives).FirstAsync(b => b.GroupType == (int)BatchGroupType.WarehouseReceive && b.Id == model.Id);

            batch.Name = model.Name;
            batch.GroupType = (int)model.GroupType;
            batch.Note = model.Note;
            if (batch.BatchWarehouseReceives == null || batch.BatchWarehouseReceives.Count == 0)
            {
                batch.BatchWarehouseReceives = new List<BatchWarehouseReceive>();
            }
            var warehouseReceiveBatch = batch.BatchWarehouseReceives.First();

            _memoryCache.Remove($"warehousereceivebatch-{batch.Id}");
            await _context.SaveChangesAsync();
            return batch;
        }

        private async Task<Batch> UpdateLoadDeliveryAsync(LoadDeliveryBatchEntity model, bool isSaving = true, params int[] orderIds)
        {
            Batch batch = await _context.Batches.Include(b => b.LoadDeliveryBatches).FirstAsync(b => b.GroupType == (int)BatchGroupType.LoadDelivery && b.Id == model.Id);

            batch.Name = model.Name;
            batch.GroupType = (int)model.GroupType;
            batch.Note = model.Note;
            if (batch.LoadDeliveryBatches == null || batch.LoadDeliveryBatches.Count == 0)
            {
                batch.LoadDeliveryBatches = new List<LoadDeliveryBatch>();
            }
            var loadDeliveryBatch = batch.LoadDeliveryBatches.First();

            _memoryCache.Remove($"loaddeliverybatch-{batch.Id}");
            await _context.SaveChangesAsync();
            return batch;
        }

        private async Task<Batch> UpdatePalletAsync(PalletBatchEntity model, bool isSaving = true, params int[] orderIds)
        {
            Batch batch = await _context.Batches.Include(b => b.BatchPallets).FirstAsync(b => b.GroupType == (int)BatchGroupType.Pallet && b.Id == model.Id);

            batch.Name = model.Name;
            batch.GroupType = (int)model.GroupType;
            batch.Note = model.Note;
            if (batch.BatchPallets == null || batch.BatchPallets.Count == 0)
            {
                batch.BatchPallets = new List<BatchPallet>();
            }
            var batchPallet = batch.BatchPallets.First();

            batchPallet.Length = model.Length;
            batchPallet.Width = model.Width;
            batchPallet.Height = model.Height;
            batchPallet.WeightKg = model.WeightKg;

            _memoryCache.Remove($"batchpallet-{batch.Id}");
            await _context.SaveChangesAsync();
            return batch;
        }

        private async Task<Batch> CreateWarehouseReceiveAsync(WarehouseReceiveBatchEntity model, bool isSaving = true, params int[] orderIds)
        {
            var batch = new Batch
            {
                Name = model.Name,
                IsFromChina = true,
                DateCreated = _date.UserNow,
                GroupType = (int)BatchGroupType.WarehouseReceive,
                UserId = _session.CurrentUser.Id,
                Note = model.Note,
                BatchWarehouseReceives = new List<BatchWarehouseReceive>
                {
                    new BatchWarehouseReceive
                    {
                        WarehouseId = model.WarehouseId,
                    },
                },
                CompanyId = Config.COMPANY_ID,
            };

            var batchBox = new BatchBox() { Number = 1 };
            if (orderIds != null && orderIds.Any())
            {
                batchBox.BatchBoxOrderMaps = orderIds.Distinct().Select(o => new BatchBoxOrderMap()
                {
                    OrderId = o
                }).ToList();
            }

            batch.BatchBoxes.Add(batchBox);
            await _context.Batches.AddAsync(batch);

            if (isSaving)
            {
                await _context.SaveChangesAsync();
            }

            return batch;
        }
        private async Task<Batch> CreateLoadDeliveryAsync(LoadDeliveryBatchEntity model, bool isSaving = true, params int[] orderIds)
        {
            var batch = new Batch
            {
                Name = model.Name,
                IsFromChina = true,
                DateCreated = _date.UserNow,
                GroupType = (int)BatchGroupType.LoadDelivery,
                UserId = _session.CurrentUser.Id,
                Note = model.Note,
                LoadDeliveryBatches = new List<LoadDeliveryBatch>
                {
                    new LoadDeliveryBatch
                    {
                        WarehouseId = model.WarehouseId,
                    },
                },
                CompanyId = Config.COMPANY_ID,
            };

            var batchBox = new BatchBox() { Number = 1 };
            if (orderIds != null && orderIds.Any())
            {
                batchBox.BatchBoxOrderMaps = orderIds.Distinct().Select(o => new BatchBoxOrderMap()
                {
                    OrderId = o
                }).ToList();
            }

            batch.BatchBoxes.Add(batchBox);
            await _context.Batches.AddAsync(batch);

            if (isSaving)
            {
                await _context.SaveChangesAsync();
            }

            return batch;
        }

        private async Task<Batch> CreatePalletAsync(PalletBatchEntity model, bool isSaving = true, params int[] orderIds)
        {
            var batch = new Batch
            {
                Name = model.Name,
                IsFromChina = true,
                DateCreated = _date.UserNow,
                GroupType = (int) BatchGroupType.Pallet,
                UserId = _session.CurrentUser.Id,
                Note = model.Note,
                BatchPallets = new List<BatchPallet>
                {
                    new BatchPallet
                    {
                        WarehouseId = model.WarehouseId,
                        Length = model.Length, 
                        Width = model.Width,
                        Height = model.Height,
                        WeightKg = model.WeightKg,
                    },
                },
                CompanyId = Config.COMPANY_ID,
            };

            var batchBox = new BatchBox() {Number = 1};
            if (orderIds != null && orderIds.Any())
            {
                batchBox.BatchBoxOrderMaps = orderIds.Distinct().Select(o => new BatchBoxOrderMap()
                {
                    OrderId = o
                }).ToList();
            }

            batch.BatchBoxes.Add(batchBox);
            await _context.Batches.AddAsync(batch);

            if (isSaving)
            {
                await _context.SaveChangesAsync();
            }

            return batch;
        }

        private async Task<Batch> CreateAsync(BatchEntity model, bool isSaving = true, params int[] orderIds)
        {
            var batch = new Batch
            {
                Name = model.Name,
                IsFromChina = true,
                DateCreated = DateTime.UtcNow,
                GroupType = (int)model.GroupType,
                IntCarrier = model.IntCarrier,
                IntNumber = model.IntNumber,
                Cost = model.Cost,
                AddOnCost = model.AddOnCost,
                Duty = model.Duty,
                StorageCost = model.StorageCost,
                Discount = model.Discount,
                InsuranceFee = model.InsuranceFee,
                HeBaoCost = model.HeBaoCost,
                TotalExpense = model.TotalExpense,
                PaidWeightKg = model.WeightKg,
                //RecipientUserId = model.RecipientId,
                BelongsToUserId = model.AgentId,
                ProgressId = model.ProgressId,
                MasterBatchId = model.MasterBatchId,
                WarehouseId = model.WarehouseId,
                RouteId = model.RouteId,
                Stage = (int)model.Stage,
                TargetWeightKg = model.TargetWeightKg,
                Commission = model.Commission,
                DateEntered = model.DateEntered,
                UserId = _session.CurrentUser.Id,
                Note = model.Note,
                CompanyId = model.GroupType == BatchGroupType.DailyScan ? null : Config.COMPANY_ID,
            };

            var batchBox = new BatchBox() { Number = 1 };
            if (orderIds != null && orderIds.Any())
            {
                batchBox.BatchBoxOrderMaps = orderIds.Distinct().Select(o => new BatchBoxOrderMap()
                {
                    OrderId = o
                }).ToList();

                if (model.RouteId.HasValue)
                {
                    model.Route = await _routeService.GetAsync(model.RouteId.Value);
                }
                var orderState = model.GetOrderState();

                if (orderState != OrderState.None)
                {
                    var orders = await _context.TransportOrders.Where(o => orderIds.Contains(o.Id)).ToListAsync();
                    foreach (var order in orders)
                    {
                        order.State = (int)orderState;
                    }
                }
            }

            batch.BatchBoxes.Add(batchBox);
            await _context.Batches.AddAsync(batch);

            if (isSaving)
            {
                await _context.SaveChangesAsync();
            }

            return batch;
        }

        public async Task<OrderScanStatusEntity> SaveOrderScanStatus(OrderScanStatusEntity model, int userId)
        {
            var scanStatus = _context.OrderScanStatus.Where(s => s.OrderId == model.OrderId);
            if (model.Status != OrderScanStatusType.SecondScan && model.Status != OrderScanStatusType.ThirdScan)
            {
                throw new ApiException("UnknownStatus", $"未知状态 {model.Status}");
            }
            if (model.Status == OrderScanStatusType.SecondScan)
            {
                if (scanStatus.Any(s => s.Status == (int)OrderScanStatusType.ThirdScan))
                {
                    throw new ApiException("ThirdScanAlreadyDone", "三次扫描过去已完成。");
                }
                if (scanStatus.Any(s => s.Status == (int)OrderScanStatusType.SecondScan))
                {
                    throw new ApiException("SecondScanAlreadyDone", "二次扫描过去已完成。");
                }
            }
            if (model.Status == OrderScanStatusType.ThirdScan)
            {
                if (scanStatus.Any(s => s.Status == (int)OrderScanStatusType.ThirdScan))
                {
                    throw new ApiException("ThirdScanAlreadyDone", "三次扫描过去已完成。");
                }
            }
            OrderScanStatus status = new OrderScanStatus
            {
                OrderId = model.OrderId,
                Status = (int)model.Status,
                Timestamp = DateTime.Now,
                UserId = userId
            };
            try
            {
                _context.OrderScanStatus.Add(status);
                // await _context.SaveChangesAsync();
                // 批次内二次和三次扫描，加 "确认扫描" 的内部操作记录
                await _orderService.AddInternalStatus(OrderStatusType.ConfirmScan, _session.CurrentUser.Id, model.OrderId);

            } catch (Exception e)
            {
                var errorText = new StringBuilder(e.Message);
                if (e.InnerException != null)
                {
                    errorText.Append(e.InnerException.Message);
                }
                throw new ApiException("UnknownError", errorText.ToString());
            }
            return model;
        }

        public IEnumerable<OrderScanStatusEntity> GetOrderScanStatusEntities(IEnumerable<int> orderIds)
        {
            if (orderIds == null || orderIds.Count() == 0) return new OrderScanStatusEntity[0];
            return _context.OrderScanStatus.FromSqlRaw($"SELECT OrderId, Status, UserId, Timestamp FROM order_scan_status WHERE OrderId IN ({string.Join(",", orderIds)})").Select(s => new OrderScanStatusEntity
            {
                OrderId = s.OrderId,
                Status = (OrderScanStatusType)s.Status,
                UserId = s.UserId,
                Timestamp = s.Timestamp
            }).Join(_context.Users, status => status.UserId, user => user.Id, (status, user) => new OrderScanStatusEntity
            {
                OrderId = status.OrderId,
                Status = status.Status,
                UserId = status.UserId,
                UserName = user.UserName,
                Timestamp = status.Timestamp
            });
        }

        public async Task<int> CreateCouponBatchAsync(string name)
        {
            var batch = new CouponBatch
            {
                CreatedById = _session.CurrentUser.Id,
                CreateTime = DateTime.UtcNow,
                Name = name
            };
            await _context.CouponBatches.AddAsync(batch);
            await _context.SaveChangesAsync();
            return batch.Id;
        }

        public async Task<CouponBatchEntity> GetCouponBatch(int id)
        {
            var batch = await _context.CouponBatches
                .Include(b => b.Coupons).ThenInclude(c => c.CouponStatuses)
                .FirstOrDefaultAsync(b => b.Id == id);
/*
@$"SELECT
  b.Id, b.Name, b.CreateTime, b.Anonymous,
  c.ShippingCost, c.CouponNumber, c.DomesticNumber, c.AssignedUserId, c.Active, c.MinimumPrice,
  s.
FROM coupon_batch b
JOIN coupon c ON b.Id=c.CouponBatchId AND b.Id=@id
LEFT JOIN user u ON c.AssignedUserId=u.Id
LEFT JOIN coupon_status s ON c.Id=s.CouponId
"
*/
            var entity = _mapper.Map<CouponBatchEntity>(batch);
            return entity;
        }

        public async Task<PagedResult<CouponBatchEntity>> ListCouponBatchAsync(FilterOptions filterOptions)
        {
            var batches = new List<CouponBatchEntity>();
            using (var conn = _context.Database.GetDbConnection())
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"
SELECT b.Id, MAX(b.Name) Name, MAX(b.CreatedById) CreatedById, MAX(b.CreateTime) CreateTime, COUNT(1) NumberOfCoupons 
FROM coupon_batch b JOIN coupon c ON b.Id=c.CouponBatchId
GROUP BY Id
ORDER BY CreateTime DESC
                    ";
                    var queryResult = await command.ExecuteReaderAsync();
                    while (queryResult.Read())
                    {
                        if (queryResult[0] != DBNull.Value)
                        {
                            batches.Add(new CouponBatchEntity
                            {
                                Id = queryResult.GetInt32(0),
                                Name = queryResult.GetString(1),
                                CreatedById = queryResult.GetInt32(2),
                                CreateTime = queryResult.GetDateTime(3),
                                NumberOfCoupons = queryResult.GetInt32(4)
                            });
                        }
                    }
                }
                conn.Close();
            }

            var total = batches.Count;

            var result = new PagedResult<CouponBatchEntity>()
            {
                Total = total,
                Items = batches
            };

            return result;
        }
    }
}
