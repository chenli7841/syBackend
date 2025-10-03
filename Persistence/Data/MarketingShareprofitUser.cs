using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class MarketingShareprofitUser
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public decimal RemainIntegral { get; set; }
        public decimal ExpendIntegral { get; set; }
        public decimal OweIntegral { get; set; }
        public bool? Type { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
