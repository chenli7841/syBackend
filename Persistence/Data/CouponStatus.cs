using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class CouponStatus
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual User User { get; set; }
    }
}
