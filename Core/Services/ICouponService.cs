using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface ICouponService
    {
        void AddStatus(CouponStatusType status, int operatorId, IEnumerable<int> orders);
        Task<CouponBatchEntity> GetAsync(int couponBatchId);
        Task DeleteCouponBatchAsync(int couponBatchId);
        Task<CouponBatchEntity> AddPhotoAsync(int couponBatchId, string rawData);
    }
}