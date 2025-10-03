using System.Threading.Tasks;
using AutoMapper;
using Domain.Services;
using Persistence.Data;
using Domain.Enums;
using Common;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Persistence.Utils;

namespace Persistence.Services
{
    public class CouponService : ICouponService
    {
        private readonly IDateTime _date;
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public CouponService(
            IDateTime date,
            EplusDbContext context,
            IMapper mapper,
            IStorageService storageService)
        {
            _date = date;
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
        }

        public void AddStatus(CouponStatusType status, int operatorId, IEnumerable<int> couponIds)
        {
            var now = _date.UserNow;
            
            foreach (var couponId in couponIds)
            {
                var couponStatus = new CouponStatus()
                {
                    CouponId = couponId,
                    DateCreated = now,
                    Status = (int)status,
                    UserId = operatorId
                };

                _context.CouponStatuses.Add(couponStatus);
            }
        }

        public async Task<CouponBatchEntity> GetAsync(int couponBatchId)
        {
            var batch = await _context.CouponBatches
                .Include(b => b.CreatedBy).ThenInclude(c => c.Customer)
                .Include(b => b.Coupons).ThenInclude(c => c.CouponStatuses)
                .Include(b => b.Coupons).ThenInclude(c => c.AssignedUser).ThenInclude(c => c.Customer)
                .Include(b => b.Coupons).ThenInclude(c => c.ConsumedUser).ThenInclude(c => c.Customer)
                .FirstOrDefaultAsync(b => b.Id == couponBatchId);
/*
@$"SELECT
  b.Id, b.Name, b.CreateTime, b.Anonymous,
  c.ShippingCost, c.CouponNumber, c.DomesticNumber, c.AssignedUserId, c.Active, c.MinimumPrice,
  s.
FROM coupon_batch b
JOIN coupon c ON b.Id=c.CouponBatchId AND b.Id=@id
LEFT JOIN user u ON c.AssignedUserId=u.Id
LEFT JOIN coupon_status s ON c.Id=s.CouponId
"
*/
            var entity = _mapper.Map<CouponBatchEntity>(batch);
            return entity;
        }

        public async Task DeleteCouponBatchAsync(int couponBatchId)
        {
            await _context.Database.ExecuteSqlRawAsync($@"
                DELETE FROM coupon WHERE Id > 0 AND CouponBatchId=@p0;
                DELETE FROM coupon_batch WHERE Id=@p0;",
                couponBatchId
            );
        }

        public async Task<CouponBatchEntity> AddPhotoAsync(int couponBatchId, string rawData)
        {
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            string random = RandomGenerator.RandomAlphaNumericString(11);
            string objectKey = $"couponbatch/{timestamp}{random}/id{couponBatchId}.png";

            var photoUrl = await _storageService.UploadAsync(rawData, objectKey);

            var couponBatch = await _context.CouponBatches.FirstOrDefaultAsync(b => b.Id == couponBatchId);
            if (couponBatch == null)
            {
                return null;
            }
            couponBatch.PhotoUrl = photoUrl;
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponBatchEntity>(couponBatch);
        }
    }
}