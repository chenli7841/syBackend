using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class MarketingReward
    {
        public long Id { get; set; }
        public int? SerialNumber { get; set; }
        public string RewardPicture { get; set; }
        public string RewardName { get; set; }
        public string RewardProbability { get; set; }
        public string MarkedWords { get; set; }
        public bool? Location { get; set; }
        public bool? RewardType { get; set; }
        public decimal? RewardCount { get; set; }
        public DateTime CreateTime { get; set; }
        public ulong IsDel { get; set; }
    }
}
