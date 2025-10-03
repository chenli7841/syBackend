using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Models.Extensions;

namespace WebUI.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public string TypeDisplayText => Type.GetDescription();
        public OrderEntity Order { get; set; }
        public BatchEntity Batch { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int? OrderId => Order?.Id;
        public string OrderName => Order?.OrderNumber;
        public string DisplayDate => Date.ToString("yyyy-MM-dd HH:mm:ss");
        public int? BatchId => Batch?.Id;
        public string BatchName => Batch?.Name;
        public decimal? CurrentBalance { get; set; }
        public UserEntity User { get; set; }
        public string Method { get; set; }
        public string Notes { get; set; }
        public int ColorIndex { get; set; }
    }
}
