using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IEmailService
    {
        Task QueueEmailDataInWarehouseAsync(int orderId, int senderUserId, int recipientUserId);
    }
}