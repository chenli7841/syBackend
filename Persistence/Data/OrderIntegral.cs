using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderIntegral
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public bool OrderStatus { get; set; }
        public bool? OrderPayType { get; set; }
        public DateTime? OrderSendGoodsTime { get; set; }
        public DateTime? OrderTakeGoodsTime { get; set; }
        public string OrderRemark { get; set; }
        public long? GoodsId { get; set; }
        public string GoodsName { get; set; }
        public decimal? GoodsPrice { get; set; }
        public string GoodsPicture { get; set; }
        public decimal GoodsChangePrice { get; set; }
        public long ClientId { get; set; }
        public string ClientNumber { get; set; }
        public string ClientAvatar { get; set; }
        public string ClientPhone { get; set; }
        public string ClientNickname { get; set; }
        public long ShopId { get; set; }
        public string ShopName { get; set; }
        public string TransportCompany { get; set; }
        public string TransportCompanyCode { get; set; }
        public string TransportNumber { get; set; }
        public string AddrConsignee { get; set; }
        public string AddrMobile { get; set; }
        public long? AddrAreaId { get; set; }
        public string AddrAreaNames { get; set; }
        public string AddrAreaCode { get; set; }
        public string AddrDetailArea { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
