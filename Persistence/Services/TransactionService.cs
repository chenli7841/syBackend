using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Services
{
    public sealed class TransactionService : ITransactionService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;

        public TransactionService(EplusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<TransactionEntity>> ListAsync(int userId, FilterOptions filterOptions)
        {
            var transactions = _context.BalanceHistories.Where(b => b.ToUserId == userId)
                .Include(b => b.FromUser).ThenInclude(u => u.Customer)
                .Include(b => b.FromUser).ThenInclude(u => u.BelongsToNavigation).ThenInclude(u => u.Customer)
                .Include(b => b.ToUser).ThenInclude(u => u.Customer)
                .Include(b => b.ToUser).ThenInclude(u => u.BelongsToNavigation).ThenInclude(u => u.Customer)
                .Include(b => b.Order)
                .Include(b => b.Batch)
                .OrderByDescending(b => b.Date);

            var total = await transactions.CountAsync();
            var pagedTransactions = transactions.Skip(filterOptions.Skip).Take(filterOptions.PageSize+25);
            var items = await pagedTransactions.Select(t => _mapper.Map<TransactionEntity>(t)).ToListAsync();
            for(var i = 0; i < items.Count - 1; i++)
            {
                if (items[i].ToUserActualAmount != null)
                {
                    items[i].Amount = items[i].ToUserActualAmount.Value;
                }
            }
            
            if (items.Count > filterOptions.PageSize)
            {
                items = items.GetRange(0, filterOptions.PageSize);
            }

            var result = new PagedResult<TransactionEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }
    }
}
