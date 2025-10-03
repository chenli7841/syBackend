using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ILocationService
    {
        Task CreateAsync(PickUpLocationEntity user, int? belongsToId);
        Task<IEnumerable<PickUpLocationAreaEntity>> ListAreas();
    }
}
