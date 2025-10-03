using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class RecordBalanceHistory
    {
        public long Id { get; set; }
        public bool RecordType { get; set; }
        public long UserId { get; set; }
        public decimal CashEntry { get; set; }
        public decimal BalanceEntry { get; set; }
        public decimal AccountOut { get; set; }
        public decimal Cumulative { get; set; }
        public ulong IsConfirm { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }
        public string Date { get; set; }
    }
}
