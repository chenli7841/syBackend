using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<UserEntity> GetAsync(string userName, string password);
        Task<UserEntity> GetAsync(string phoneNumber);
        Task<UserEntity> GetAsync(int id);
        Task<PagedResult<UserEntity>> SearchByUserCodeAsync(string code, int pageSize, int[] companyIds);
        Task<PagedResult<UserEntity>> ListAsync(UserListFilterOptions filterOptions, bool isOrderByCode = true);
        Task<List<UserEntity>> ListByBatchesAsync(BatchGroupType groupType, int? routeId, int? warehouseId, int[] companyIds = null);
        Task<IEnumerable<UserEntity>> ListAgentsAsync();
        Task<IEnumerable<PickUpLocationEntity>> ListPickUpLocationsAsync(int version = 1, int[] companyIds = null);
        Task<int> TogglePickUpLocationVisibilityAsync(int id);
        Task UpdatePickupLocation(int id, string name, string address, decimal districtAdditionalRate, int sequence, string note);
        Task TransferUser(int fromPickupLocationId, int toPickupLocationId);
        //Task<UserEntity> CreateAsync(UserEntity user);
        Task<UserEntity> SaveAsync(UserEntity user);
        Task<IEnumerable<UserRoute>> ListRouteAsync(int id);
        Task SetRouteVisibilityAsync(int userId, int routeId, bool isVisible);
        Task SetUserRoleAsync(int userId, string roleCode, bool enabled);
        Task<string> GetShippingAddressAsync(int id);
        Task ChangePassword(int userId, string password);
        void Transfer(int fromUserId, int toUserId, decimal amount, TransactionType transactionType, int? batchId);

        decimal Deposit(BalanceTransferInfo info);
        Task<decimal> GetBalanceSummaryAsync();
        Task DeletePickupLocation(int id);
        Task<IEnumerable<RoleEntity>> ListRolesAsync(string[] exclude);
    }
}