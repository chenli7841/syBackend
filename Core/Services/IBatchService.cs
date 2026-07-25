using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IBatchService
    {
        Task<PagedResult<BatchEntity>> ListWarehouseReceiveBatchAsync(BatchListFilterOptions filterOptions, int[] companyIds);
        Task<PagedResult<BatchEntity>> ListLoadDeliveryBatchAsync(BatchListFilterOptions filterOptions, int[] companyIds);
        Task<PagedResult<BatchEntity>> ListPalletBatchAsync(BatchListFilterOptions filterOptions, int[] companyIds);
        Task<PagedResult<BatchEntity>> ListAsync(BatchListFilterOptions filterOptions, int[] companyIds);
        Task<PagedResult<PackageBatchEntity>> ListPackageBatchAsync(BatchListFilterOptions filterOptions, int[] companyIds);
        Task<PagedResult<PendingDispatchBatchEntity>> ListPendingDispatchAsync(BatchListFilterOptions filterOptions, int[] companyIds);
        Task<PagedResult<BatchEntity>> ListPalletAsync(BatchListFilterOptions filterOptions, int[] companyIds);
        Task<IEnumerable<BatchEntity>> ListMasterBatchesAsync(BatchGroupType groupType, int? routeId, int[] companyIds = null, BatchStageType? stage = null, int? warehouseId = null, BatchStageType[] stages = null);
        Task<PagedResult<BatchOtherOrderEntity>> ListOtherOrderAsync(BatchListOtherOrderFilterOptions filterOptions);
        Task<IEnumerable<RouteBatchCount>> GetBatchCountByRouteAsync(BatchGroupType groupType, int[] companyIds);
        Task<IEnumerable<BatchEntity>> GetByOrderAsync(int orderId);
        Task<BatchEntity> GetAsync(int id);
        Task<BatchEntity> GetSummaryAsync(int id);
        Task<BatchEntity> GetForPrintAsync(int id);
        Task<BatchEntity> GetForEditAsync(int id, int[] companyIds = null);
        Task<PackageBatchEntity> GetForEditPackageAsync(int id);
        Task<PalletBatchEntity> GetForEditPalletAsync(int id);
        Task<BatchEntity> GetByBoxIdAsync(int id);
        Task<BatchEntity> GetForAddOrderAsync(int boxId);
        Task<BatchEntity> GetForEditBoxAsync(int boxId);
        Task<BatchEntity> AddOrderAsync(int boxId, int orderId, OrderEntity order = null);
        Task<BatchEntity> AddOrderToPackageBatchAsync(int boxId, int orderId, OrderEntity order);
        Task AddOtherOrderAsync(int boxId, string number, int userId);
        Task RemoveOrderAsync(int boxId, int orderId);
        Task<BatchEntity> SaveAsync(BatchEntity model);
        Task<BatchEntity> SavePackageBatchAsync(PackageBatchEntity model);
        Task<PalletBatchEntity> SavePalletAsync(PalletBatchEntity model);
        Task<LoadDeliveryBatchEntity> SaveLoadDeliveryAsync(LoadDeliveryBatchEntity model);
        Task<WarehouseReceiveBatchEntity> SaveWarehouseReceiveAsync(WarehouseReceiveBatchEntity model);
        Task AddBoxAsync(int id, int boxNumber);
        Task CreateDailyBatchPerWarehouseAsync(BatchGroupType groupType);
        Task MoveNextAsync(int id);
        Task Pay(int batchId, decimal totalExpense);
        Task PayAndMoveNextAsync(int id, PayType payType);
        Task UpdateOrdersLoadDeliveryProperties(int batchId);
        Task SplitAsync(int id);
        Task SplitByLocationsAsync(int id);
        Task SplitByRecipientsAsync(int id);
        Task SplitByAgentsAsync(int id);
        Task SplitByNonLocation(int id);
        Task SplitByNonAgent(int id);
        Task DeleteAsync(int id);
        Task MergeAsync(int targetBatchId, int sourceBatchId, int? sourceBoxNumber, string originalBoxNumber);
        Task AcceptBoxAsync(int targetBatchId, int sourceBatchId, int? sourceBoxNumber, string originalBoxNumber);
        Task AcceptOrderAsync(int targetBatchId, string orderOrDomesticNumber);
        Task AcceptOrderToPalletAsync(int targetBatchId, string orderOrDomesticNumber);
        Task CommissionAsync(int id);
        void RemoveCache(int id);
        Task<OrderScanStatusEntity> SaveOrderScanStatus(OrderScanStatusEntity model, int userId);
        IEnumerable<OrderScanStatusEntity> GetOrderScanStatusEntities(IEnumerable<int> orderIds);
        Task<OrderCostSummaryEntity> GetOrderCostSummary(int batchId);
        Task<int> CreateCouponBatchAsync(string name);
        Task<PagedResult<CouponBatchEntity>> ListCouponBatchAsync(FilterOptions filterOptions);
        Task<CouponBatchEntity> GetCouponBatch(int id);
        Task UpdateBatchBox(int boxId, double? length, double? width, double? height, double? actualWeightKg);
    }
}
