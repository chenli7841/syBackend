using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services
{
    public interface IChinaStatusService
    {
        Task<ChinaOrder> GetStatusAsync(string carrier, string number, string phone);
        Task<bool> SubscribeStatus(string carrier, string number);
        ChinaCarrier DetectCarrier(string number);
    }
}