using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsOrderShop
    {
        public long Id { get; set; }
        public string ShopOrderNumber { get; set; }
        public long OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal OrderShopSubtotal { get; set; }
        public decimal OrderGoodsSubtotal { get; set; }
        public decimal OrderIntegralSubtotal { get; set; }
        public bool OrderStatus { get; set; }
        public ulong OrderIsFinish { get; set; }
        public DateTime? OrderSendGoodsTime { get; set; }
        public DateTime? OrderTakeGoodsTime { get; set; }
        public long ShopId { get; set; }
        public string ShopName { get; set; }
        public long? SupplierId { get; set; }
        public ulong? TransportIsSupplier { get; set; }
        public decimal TransportCost { get; set; }
        public bool TransportType { get; set; }
        public string TransportPhone { get; set; }
        public string TransportExplain { get; set; }
        public string TransportCompany { get; set; }
        public string TransportCompanyCode { get; set; }
        public string TransportNumber { get; set; }
        public string TransportNumberImage { get; set; }
        public string AddrConsignee { get; set; }
        public string AddrMobile { get; set; }
        public long? AddrAreaId { get; set; }
        public string AddrAreaNames { get; set; }
        public string AddrAreaCode { get; set; }
        public string AddrDetailArea { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
