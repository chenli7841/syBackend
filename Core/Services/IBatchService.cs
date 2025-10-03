using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IBatchService
    {
        Task<PagedResult<BatchEntity>> ListAsync(BatchListFilterOptions filterOptions);
        Task<IEnumerable<BatchEntity>> ListMasterBatchesAsync(BatchGroupType groupType, int? routeId);
        Task<PagedResult<BatchOtherOrderEntity>> ListOtherOrderAsync(BatchListOtherOrderFilterOptions filterOptions);
        Task<IEnumerable<RouteBatchCount>> GetBatchCountByRouteAsync(BatchGroupType groupType);
        Task<IEnumerable<BatchEntity>> GetByOrderAsync(int orderId);
        Task<BatchEntity> GetAsync(int id);
        Task<BatchEntity> GetForPrintAsync(int id);
        Task<BatchEntity> GetForEditAsync(int id);
        Task<BatchEntity> GetByBoxIdAsync(int id);
        Task<BatchEntity> GetForAddOrderAsync(int boxId);
        Task<BatchEntity> GetForEditBoxAsync(int boxId);
        Task<BatchEntity> AddOrderAsync(int boxId, int orderId, OrderEntity order = null);
        Task AddOtherOrderAsync(int boxId, string number);
        Task RemoveOrderAsync(int boxId, int orderId);
        Task<BatchEntity> SaveAsync(BatchEntity model);
        Task AddBoxAsync(int id, int boxNumber);
        Task CreateDailyBatchPerWarehouseAsync(BatchGroupType groupType);
        Task MoveNextAsync(int id);
        Task PayAndMoveNextAsync(int id, PayType payType);
        Task UpdateOrdersLoadDeliveryProperties(int batchId);
        Task SplitAsync(int id);
        Task SplitByLocationsAsync(int id);
        Task SplitByRecipientsAsync(int id);
        Task SplitByAgentsAsync(int id);
        Task SplitByNonLocation(int id);
        Task SplitByNonAgent(int id);
        Task DeleteAsync(int id);
        Task MergeAsync(int targetBatchId, int sourceBatchId, int? sourceBoxNumber);
        Task CommissionAsync(int id);
        void RemoveCache(int id);
        Task<OrderScanStatusEntity> SaveOrderScanStatus(OrderScanStatusEntity model, int userId);
        IEnumerable<OrderScanStatusEntity> GetOrderScanStatusEntities(IEnumerable<int> orderIds);
        Task<OrderCostSummaryEntity> GetOrderCostSummary(int batchId);
        Task<int> CreateCouponBatchAsync(string name);
        Task<PagedResult<CouponBatchEntity>> ListCouponBatchAsync(FilterOptions filterOptions);
        Task<CouponBatchEntity> GetCouponBatch(int id);
    }
}
