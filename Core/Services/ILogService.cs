using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ILogService
    {
        Task SaveSMSLog(int? batchId, int userId, string message, string phoneNumber, string content);
        Task<SMSLogEntity[]> GetSMSLogsAsync(int start, int size);
    }
}