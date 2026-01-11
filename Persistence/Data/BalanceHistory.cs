using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BalanceHistory
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
        public int? OrderId { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public string Notes { get; set; }
        public decimal? FromUserDisplayAmount { get; set; }
        public decimal? ToUserActualAmount { get; set; }
        public decimal? FromUserCurrentBalance { get; set; }
        public decimal? ToUserCurrentBalance { get; set; }
        public string Method { get; set; }
        public decimal? Rmb { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? Discount { get; set; }
        public int? BatchId { get; set; }
        public string TransactionGuid { get; set; }
        public decimal? ActualAmount { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual User FromUser { get; set; }
        public virtual TransportOrder Order { get; set; }
        public virtual User ToUser { get; set; }
    }
}
