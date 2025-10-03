using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class MarketingGoodsIntegral
    {
        public long Id { get; set; }
        public string GoodsName { get; set; }
        public decimal GoodsPrice { get; set; }
        public decimal ChangePrice { get; set; }
        public int SalesVolume { get; set; }
        public int GoodsInventory { get; set; }
        public string GoodsPicture { get; set; }
        public string GoodsDetails { get; set; }
        public long ShopId { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public ulong IsShelf { get; set; }
        public ulong IsRecommend { get; set; }
        public ulong? IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long CategoryId { get; set; }
    }
}
