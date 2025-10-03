using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysShippingAddress
    {
        public long Id { get; set; }
        public string Consignee { get; set; }
        public string Mobile { get; set; }
        public long? AreaId { get; set; }
        public string DetailArea { get; set; }
        public ulong? IsDefault { get; set; }
        public long? AppUserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
        public string WeChat { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public bool? AddressType { get; set; }
        public string PostalCode { get; set; }
        public string IdCardFrontUrl { get; set; }
        public string IdCardBackUrl { get; set; }
        public string IdCardNumber { get; set; }
        public string LatAndLng { get; set; }
    }
}
