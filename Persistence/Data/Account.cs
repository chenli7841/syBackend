using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Account
    {
        public long Id { get; set; }
        public bool AccountType { get; set; }
        public long UserId { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal FreezeAmount { get; set; }
        public decimal UnliquidatedAmount { get; set; }
        public decimal Integral { get; set; }
        public decimal UnliquidatedIntegral { get; set; }
        public decimal IntegralAmount { get; set; }
        public decimal PasscardAmount { get; set; }
        public decimal PasscardBalance { get; set; }
        public decimal CautionMoney { get; set; }
        public DateTime? UpdateTime { get; set; }
        public ulong IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal? TodayBalance { get; set; }
        public decimal? TodayIntegral { get; set; }
        public ulong? IsSign { get; set; }
        public ulong? IsWatchVideo { get; set; }
        public DateTime? SignTime { get; set; }
        public DateTime? WatchVideoTime { get; set; }
        public decimal WithdrawTotal { get; set; }
    }
}
