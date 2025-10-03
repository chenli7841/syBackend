using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderRefundFlow
    {
        public long Id { get; set; }
        public string RefundNumber { get; set; }
        public bool RefundStatus { get; set; }
        public string OptionMessage { get; set; }
        public string Remark { get; set; }
        public long UserId { get; set; }
        public bool UserType { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
