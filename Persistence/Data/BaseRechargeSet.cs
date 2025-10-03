using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BaseRechargeSet
    {
        public long Id { get; set; }
        public ulong? IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal RechargeAmount { get; set; }
        public decimal? GiveBalance { get; set; }
        public decimal GiveIntegral { get; set; }
        public bool RechargeType { get; set; }
    }
}
