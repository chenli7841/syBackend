using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain.Enums;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Services
{
    public class AdminDataService : IAdminDataService
    {
        private readonly EplusDbContext _context;
        private readonly IDateTime _date;

        public AdminDataService(EplusDbContext context, IDateTime date)
        {
            _context = context;
            _date = date;
        }

        public async Task HashPasswordsAsync()
        {
            var users = await _context.Users.Where(u => u.Id != 13).ToListAsync();

            foreach (var user in users)
            {
                var encoding = new UTF8Encoding();
                var passwordAsBytes = encoding.GetBytes(user.Password + user.Id);

                var sha512 = new SHA512Managed();
                sha512.ComputeHash(passwordAsBytes);

                user.Password = Convert.ToBase64String(sha512.Hash);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> SetOrderNumberAsync()
        {
            var orders = await _context.TransportOrders.Include(o => o.CreatedBy).Include(o => o.RouteNavigation)
                .Where(o => o.State == (int) OrderState.Draft && o.RouteId.HasValue && string.IsNullOrEmpty(o.OrderNumber)).ToListAsync();

            var tick = 1;
            foreach (var order in orders)
            {
                var lapsedSeconds = (ulong)Math.Round((_date.UserNow - _date.OrderStartTime).TotalSeconds + (tick++));
                order.OrderNumber = order.RouteNavigation.Code + order.CreatedBy.OrderStartNumber + lapsedSeconds.ToString().PadLeft(10, '0');
            }

            await _context.SaveChangesAsync();
            return orders.Count;
        }

        public async Task<int> PopulateOrderPickUpLocation()
        {
            var batch = await _context.Batches.Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.BelongsToNavigation).ThenInclude(u => u.Customer)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.Customer)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                .ThenInclude(m => m.Order).ThenInclude(o => o.CreatedBy).ThenInclude(u => u.PickUpLocationNavigation).ThenInclude(u => u.BelongsTo)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                .ThenInclude(m => m.Order).ThenInclude(o => o.PickUpLocation).ThenInclude(p => p.BelongsTo)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                .ThenInclude(m => m.Order).ThenInclude(o => o.ChinaItems)
                .Include(b => b.BatchBoxes).ThenInclude(bx => bx.BatchBoxOrderMaps)
                .ThenInclude(m => m.Order).ThenInclude(o => o.OrderStatuses)
                .Include(b => b.MasterBatch).ThenInclude(m => m.Progress).ThenInclude(p => p.Route)
                .Include(b => b.Route)
                .Include(b => b.User).ThenInclude(u => u.Customer)
                .Include(b => b.BatchOtherOrders)
                .FirstAsync(b => b.Id == 706);

            var orders = batch.BatchBoxes.SelectMany(box => box.BatchBoxOrderMaps).Select(map => map.Order).ToList();
            foreach (var order in orders)
            {
                order.PickUpLocationId = order.CreatedBy.PickUpLocationId;
            }

            await _context.SaveChangesAsync();

            return orders.Count;
        }
    }
}