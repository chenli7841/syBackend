using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsOrderInvoice
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public string OrderNumber { get; set; }
        public long? ShopOrderId { get; set; }
        public string ShopOrderNumber { get; set; }
        public long ShopId { get; set; }
        public long? ClientId { get; set; }
        public string ClientAvatar { get; set; }
        public string ClientPhone { get; set; }
        public string ClientNickName { get; set; }
        public bool Type { get; set; }
        public bool? GeneralType { get; set; }
        public ulong IsOpen { get; set; }
        public DateTime? OpenInvoTime { get; set; }
        public string Name { get; set; }
        public string TaxpayersNum { get; set; }
        public string RegisterAddress { get; set; }
        public string RegisterPhone { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public ulong IsDel { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
