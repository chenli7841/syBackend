using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Coupon
    {
        public int Id { get; set; }
        public decimal ShippingCost { get; set; }
        public string CouponNumber { get; set; }
        public string DomesticNumber { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatedById { get; set; }
        public int CouponBatchId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public int? AssignedUserId { get; set; }
        public int? ConsumedUserId { get; set; }
        public decimal MinimumPrice { get; set; }
        // 0代表未设定，1代表不记名优惠券类型，2代表记名优惠券类型
        public int CouponType { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual CouponBatch CouponBatch { get; set; }
        public virtual ICollection<CouponStatus> CouponStatuses { get; set; }
        public virtual User AssignedUser { get; set; }
        public virtual User ConsumedUser { get; set; }
    }
}
