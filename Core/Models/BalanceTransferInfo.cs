using Domain.Enums;

namespace Domain.Models
{
    public class BalanceTransferInfo
    {
        public string TransferType { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Notes { get; set; }
        public PayType PayType { get; set; }
        public decimal? Rmb { get; set; }
        public decimal? ExchangeRate { get; set; }
        public int? OrderId { get; set; }
        public int? BatchId { get; set; }
        public decimal? Discount { get; set; }
    }
}
