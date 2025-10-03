using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class MarketingGroupOrder
    {
        public long Id { get; set; }
        public ulong? IsDel { get; set; }
        public string ActivityName { get; set; }
        public long ActivityId { get; set; }
        public DateTime CreateTime { get; set; }
        public long GoodsId { get; set; }
        public string GoodsPicture { get; set; }
        public string GoodsName { get; set; }
        public decimal? GoodsPrice { get; set; }
        public string UserAvatar { get; set; }
        public string NickName { get; set; }
        public string UserNumber { get; set; }
        public bool? PayType { get; set; }
        public string ShopName { get; set; }
        public long ShopId { get; set; }
        public long AppUserId { get; set; }
        public bool? ActivityStatus { get; set; }
        public string OrderNumber { get; set; }
        public string PayNumber { get; set; }
        public DateTime? PayTime { get; set; }
        public decimal OrderTotal { get; set; }
        public bool? OrderStatus { get; set; }
        public bool? OrderPayType { get; set; }
        public string OrderRemark { get; set; }
        public DateTime? OrderSendGoodsTime { get; set; }
        public DateTime? OrderTakeGoodsTime { get; set; }
        public string TransportCompany { get; set; }
        public string TransportCompanyCode { get; set; }
        public string TransportNumber { get; set; }
        public string AddrConsignee { get; set; }
        public string AddrMobile { get; set; }
        public long? AddrAreaId { get; set; }
        public string AddrAreaNames { get; set; }
        public string AddrAreaCode { get; set; }
        public string AddrDetailArea { get; set; }
        public string RefundNumber { get; set; }
        public decimal? TransportCost { get; set; }
        public long? InvoiceId { get; set; }
        public bool? InvoiceType { get; set; }
        public bool? GeneralType { get; set; }
        public string InvoiceName { get; set; }
        public string TaxpayersNum { get; set; }
        public long? SupplierId { get; set; }
        public decimal? Integral { get; set; }
    }
}
