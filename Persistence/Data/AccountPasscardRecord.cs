using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class AccountPasscardRecord
    {
        public long Id { get; set; }
        public string BillNumber { get; set; }
        public bool TransactionType { get; set; }
        public bool InOut { get; set; }
        public bool AccountType { get; set; }
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal PasscardBalance { get; set; }
        public bool OtherAccountType { get; set; }
        public long OtherAccountId { get; set; }
        public short? BusinessType { get; set; }
        public string Remark { get; set; }
        public long? OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public long? OperatorId { get; set; }
        public string OperatorName { get; set; }
        public DateTime CreateTime { get; set; }
        public bool? RecordType { get; set; }
        public decimal PasscardAccount { get; set; }
        public string UserPhone { get; set; }
    }
}
