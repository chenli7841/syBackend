using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface IDeliverProgressService
    {
        Task<IEnumerable<DeliverProgressEntity>> ListAsync();
        Task<DeliverProgressEntity> GetAsync(int id);
        Task<DeliverProgressEntity> SaveAsync(DeliverProgressEntity model);
        Task DeleteAsync(int id);
    }
}
