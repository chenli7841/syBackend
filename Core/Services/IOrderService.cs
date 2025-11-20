using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IOrderService
    {
        Task<PagedResult<OrderEntity>> ListAsync(OrderListFilterOptions filterOptions);
        void ClearCache(int id);
        Task<OrderEntity> GetAsync(int id);
        Task AddStatus(OrderStatusType status, int operatorId, params OrderEntity[] orders);
        Task AddInternalStatus(OrderStatusType status, int operatorId, params int[] orders);
        Task<string> UpdateChinaStatus();
        Task Delete(int id);
        Task<OrderEntity> FindAsync(string number, bool currentCompany);
        Task<OrderEntity> SaveAsync(OrderEntity entity);
        Task SetOrderState(int id, OrderState state, string reason);
        Task<OrderEntity> SaveDraftAsync(OrderEntity entity);
        Task<int> BatchCreateOrderAsync(string prefix, string startNumber, string endNumber, int routeId, int batchId, string validFrom, string validUntil, decimal shippingCost, decimal minimumPrice);
        Task<decimal> CalculateItemCostAsync(OrderEntity order);
        Task ReturnCompleteAsync(int id);
        Task<OrderPhotoEntity> AddPhotoAsync(int orderId, string rawData);
        Task DeletePhotoAsync(int photoId);
    }
}
