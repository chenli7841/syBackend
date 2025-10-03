using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class CouponEntity
    {
        public int Id { get; set; }
        public decimal ShippingCost { get; set; }
        /// <summary>国内单号</summary>
        public string CouponNumber { get; set; }
        /// <summary>单号</summary>
        public string DomesticNumber { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatedById { get; set; }
        public int CouponBatchId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public int? AssignedUserId { get; set; }
        public decimal MinimumPrice { get; set; }
        public UserEntity AssignedUser { get; set; }
        public UserEntity ConsumedUser { get; set; }
        public IEnumerable<CouponStatusEntity> Status { get; set; }
    }

    public class CouponStatusEntity
    {
        public CouponStatusType Status { get; set; }
        public DateTime Date { get; set; }
        public UserEntity Operator { get; set; }
    }
}