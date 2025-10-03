using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class MarketingRewardRecord
    {
        public long Id { get; set; }
        public int? SerialNumber { get; set; }
        public string OrderNumber { get; set; }
        public long? RewardId { get; set; }
        public long? UserId { get; set; }
        public int? RewardType { get; set; }
        public bool? RewardStaus { get; set; }
        public bool? TransportType { get; set; }
        public string TransportPhone { get; set; }
        public string TransportCompany { get; set; }
        public string TransportCompanyCode { get; set; }
        public string TransportNumber { get; set; }
        public string TransportExplain { get; set; }
        public string AddrConsignee { get; set; }
        public string AddrMobile { get; set; }
        public long? AddrAreaId { get; set; }
        public string AddrAreaNames { get; set; }
        public string AddrDetailArea { get; set; }
        public DateTime? OrderSendGoodsTime { get; set; }
        public DateTime? OrderTakeGoodsTime { get; set; }
        public ulong? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
