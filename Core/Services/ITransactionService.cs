using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;

namespace Domain.Services
{
    public interface ITransactionService
    {
        Task<PagedResult<TransactionEntity>> ListAsync(int userId, FilterOptions filterOptions);
    }
}
