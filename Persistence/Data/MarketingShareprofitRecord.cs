using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class MarketingShareprofitRecord
    {
        public int Id { get; set; }
        public long? UserId { get; set; }
        public decimal? RemainIntegral { get; set; }
        public decimal? ExpendIntegral { get; set; }
        public decimal? RealPasscardProfits { get; set; }
        public decimal? OwePasscard { get; set; }
        public bool? Type { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
