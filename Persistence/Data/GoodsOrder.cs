using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsOrder
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderRemark { get; set; }
        public bool? OrderPayType { get; set; }
        public DateTime? OrderPayTime { get; set; }
        public decimal? OrderTotal { get; set; }
        public decimal OrderIntegralTotal { get; set; }
        public bool OrderGoodsType { get; set; }
        public long ClientId { get; set; }
        public string ClientAvatar { get; set; }
        public string ClientPhone { get; set; }
        public string ClientNickName { get; set; }
        public ulong ClientIsDel { get; set; }
        public bool InvoType { get; set; }
        public bool? InvoGeneralType { get; set; }
        public ulong InvoIsOpen { get; set; }
        public string InvoName { get; set; }
        public string InvoTaxpayersNum { get; set; }
        public string InvoRegisterAddress { get; set; }
        public string InvoRegisterPhone { get; set; }
        public string InvoBankName { get; set; }
        public string InvoBankAccount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
