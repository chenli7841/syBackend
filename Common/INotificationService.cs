using System.Threading.Tasks;

namespace Common
{
    public interface INotificationService
    {
        Task SendMessageAsync(string phoneNumber, string message, string senderName);
    }
}