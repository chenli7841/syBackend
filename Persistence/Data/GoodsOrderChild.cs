using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsOrderChild
    {
        public long Id { get; set; }
        public string GoodsOrderNumber { get; set; }
        public long ShopOrderId { get; set; }
        public string ShopOrderNumber { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal OrderActualPrice { get; set; }
        public decimal OrderIntegral { get; set; }
        public long GoodsId { get; set; }
        public decimal GoodsPrice { get; set; }
        public string GoodsName { get; set; }
        public string GoodsPicture { get; set; }
        public int GoodsNumber { get; set; }
        public long? SkuId { get; set; }
        public string SkuNumber { get; set; }
        public string SkuSpec { get; set; }
        public DateTime CreateTime { get; set; }
        public bool GoodsType { get; set; }
    }
}
