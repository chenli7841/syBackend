using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseEntity>> ListAsync();
        Task<WarehouseEntity> GetAsync(int id);
        Task<WarehouseEntity> SaveAsync(WarehouseEntity model, string photoData);
        Task DeleteAsync(int id);
    }
}
