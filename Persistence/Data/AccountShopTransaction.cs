using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class AccountShopTransaction
    {
        public long Id { get; set; }
        public long ShopId { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal? StartBalance { get; set; }
        public decimal? EndBalance { get; set; }
        public decimal? IncomeBalance { get; set; }
        public decimal? ExpendBalance { get; set; }
        public int? IncomeNumber { get; set; }
        public int? ExpendNumber { get; set; }
        public bool TransactionType { get; set; }
    }
}
