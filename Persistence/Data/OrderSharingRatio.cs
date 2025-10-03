using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderSharingRatio
    {
        public long Id { get; set; }
        public bool OrderType { get; set; }
        public long OrderId { get; set; }
        public long ClientId { get; set; }
        public decimal ClientIntegral { get; set; }
        public long ShopId { get; set; }
        public decimal ShopAmount { get; set; }
        public long? MemberFirstId { get; set; }
        public decimal MemberFirstAmount { get; set; }
        public long? MemberSecondId { get; set; }
        public decimal MemberSecondAmount { get; set; }
        public long? OnlineFirstId { get; set; }
        public decimal OnlineFirstAmount { get; set; }
        public long? OnlineSecondId { get; set; }
        public decimal? OnlineSecondAmount { get; set; }
        public long? SupplierFirstId { get; set; }
        public decimal SupplierFirstAmount { get; set; }
        public long? SupplierSecondId { get; set; }
        public decimal SupplierSecondAmount { get; set; }
        public decimal PlatformAmount { get; set; }
        public bool ShareStatus { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
