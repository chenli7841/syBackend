using System;

namespace Domain.Entities
{
    public class DepositSummaryEntity
    {
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }

    public class OtherDepositDetailEntity
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Method { get; set; }
        public decimal SenderBalance { get; set; }
        public decimal RecipientBalance { get; set; }
    }

    public class SelfDepositDetailEntity
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string User { get; set; }
        public string Method { get; set; }
        public decimal Balance { get; set; }
    }
}
