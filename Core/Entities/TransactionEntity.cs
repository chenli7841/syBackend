using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public UserEntity FromUser { get; set; }
        public UserEntity ToUser { get; set; }
        public decimal FromUserCurrentBalance { get; set; }
        public decimal ToUserCurrentBalance { get; set; }
        public decimal? FromUserDisplayAmount { get; set; }
        public decimal? ToUserActualAmount { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public OrderEntity Order { get; set; }
        public decimal? ActualAmount { get; set; }
        public BatchEntity Batch { get; set; }
        public string Method { get; set; }
        public string Notes { get; set; }
    }
}
