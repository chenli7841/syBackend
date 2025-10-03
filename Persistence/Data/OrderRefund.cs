using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderRefund
    {
        public long Id { get; set; }
        public string RefundNumber { get; set; }
        public decimal RefundPrice { get; set; }
        public decimal RealRefundPrice { get; set; }
        public decimal RefundIntegral { get; set; }
        public decimal RealRefundIntegral { get; set; }
        public long? OrderId { get; set; }
        public string OrderNumber { get; set; }
        public bool OrderType { get; set; }
        public ulong IsReturnGoods { get; set; }
        public ulong IsIntervene { get; set; }
        public long ClientId { get; set; }
        public long? ShopId { get; set; }
        public bool RefundStatus { get; set; }
        public bool? RefundPayType { get; set; }
        public bool? RefundCause { get; set; }
        public string RefundImages { get; set; }
        public ulong AddressIsSupplier { get; set; }
        public string Consignee { get; set; }
        public string Mobile { get; set; }
        public string AreaCode { get; set; }
        public string DetailArea { get; set; }
        public long? Auditor { get; set; }
        public bool? AuditorType { get; set; }
        public DateTime? AuditTime { get; set; }
        public long? Accountant { get; set; }
        public DateTime? RefundTime { get; set; }
        public string Remark { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public string ExpressCompanyCode { get; set; }
        public string ExpressNumber { get; set; }
    }
}
