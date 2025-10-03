using System.Threading.Tasks;
using Domain.Models;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
    public interface ISmsService
    {
        Task<bool> SendAsync(IEnumerable<SmsRequest> requests, int userId, int? batchId = null);
        Task SendSmsAndEmailAsync(int userId, int batchId, string customMessage, string pickUpLocation, string pickUpTime);
        Task<SupportUserEntity> GetSupportUserAsync(int userId);
        Task<IEnumerable<SmsUserInfo>> GetSmsUserInfosByBatchIdAsync(int batchId);
        Task<SmsUserInfo> GetSmsUserInfoByUserIdAsync(int userId);
        Task<IEnumerable<SmsUserInfo>> GetSmsUserInfoByBelongsToAsync(string belongsToUserOrderStartNumber);
        Task<SmsUserInfo> GetSmsUserInfoByOrderIdAsync(int orderId);
    }
}