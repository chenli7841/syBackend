using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class AccountWithdrawExamine
    {
        public long Id { get; set; }
        public string BillNumber { get; set; }
        public long AppUserId { get; set; }
        public bool AccountType { get; set; }
        public bool WithdrawalType { get; set; }
        public decimal? AccountBalance { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public decimal WithdrawalMoney { get; set; }
        public decimal TransformIntegral { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remark { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long? OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string ExamineDetails { get; set; }
        public bool WithdrawalStatus { get; set; }
        public string AppUserPhone { get; set; }
        public string BankCardBelongs { get; set; }
        public string BankCardNumber { get; set; }
        public long RecordId { get; set; }
    }
}
