using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;

namespace Domain.Services
{
    public interface IRouteService
    {
        Task<IEnumerable<RouteEntity>> ListAsync(int[] companyId = null);
        Task<RouteEntity> GetAsync(int id, bool checkCompany = true, int? companyId = null);
        Task<RouteEntity> SaveAsync(RouteEntity model, string photoData);
        Task HideAsync(int id);
        Task ShowAsync(int id);
        Task ToggleIsRegular(int id);
        Task<RoutePermissions> ListPermissionsAsync(int id);
        Task RemovePermissionsAsync(int id);
        Task AddAllPermissionsAsync(int id);
        Task DeleteAsync(int id);
    }
}
