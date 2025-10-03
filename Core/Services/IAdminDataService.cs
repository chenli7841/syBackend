using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAdminDataService
    {
        Task HashPasswordsAsync();
        Task<int> SetOrderNumberAsync();
        Task<int> PopulateOrderPickUpLocation();
    }
}