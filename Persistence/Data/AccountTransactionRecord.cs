using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class AccountTransactionRecord
    {
        public long Id { get; set; }
        public bool TransactionType { get; set; }
        public bool InOut { get; set; }
        public bool? AccountType { get; set; }
        public long? AccountId { get; set; }
        public string UserPhone { get; set; }
        public decimal Money { get; set; }
        public decimal? AccountBalance { get; set; }
        public bool? OtherAccountType { get; set; }
        public long? OtherAccountId { get; set; }
        public string OtherUserPhone { get; set; }
        public bool? Status { get; set; }
        public string Remark { get; set; }
        public string Cardid { get; set; }
        public long? OrderId { get; set; }
        public string OrderNumber { get; set; }
        public bool? OrderType { get; set; }
        public string OtherOrderId { get; set; }
        public long? RecommendedId { get; set; }
        public string BillNumber { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? BusinessType { get; set; }
        public bool? PayType { get; set; }
        public long? OperatorId { get; set; }
        public string OperatorName { get; set; }
        public ulong? IsExport { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
