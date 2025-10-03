using System.Threading.Tasks;
using Domain.Services;
using Persistence.Data;
using System;

namespace Persistence.Services
{
    public class EmailService : IEmailService
    {
        private readonly EplusDbContext _context;

        public EmailService(EplusDbContext context)
        {
            _context = context;
        }

        public async Task QueueEmailDataInWarehouseAsync(int orderId, int senderUserId, int recipientUserId)
        {
            _context.EmailDataInWarehouses.Add(new EmailDataInWarehouse
            {
                OrderId = orderId,
                SenderUserId = senderUserId,
                RecipientUserId = recipientUserId,
                DateCreated = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }
    }
}