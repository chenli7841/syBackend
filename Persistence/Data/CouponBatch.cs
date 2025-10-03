using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class CouponBatch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatedById { get; set; }
        public bool? Anonymous { get; set; }
        public string PhotoUrl { get; set; }
        public string EmailContent { get; set; }
        public string SmsContent { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
    }
}
