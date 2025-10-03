using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsOrderRefund
    {
        public long Id { get; set; }
        public long OrderRefundId { get; set; }
        public long GoodsOrderId { get; set; }
    }
}
