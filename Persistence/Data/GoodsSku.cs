using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsSku
    {
        public long Id { get; set; }
        public string SpecSkuId { get; set; }
        public long GoodsId { get; set; }
        public decimal GoodsPrice { get; set; }
        public int StockNumber { get; set; }
        public int? WarningNumber { get; set; }
        public string SkuNumber { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public string SkuPictureKey { get; set; }
        public decimal Integral { get; set; }
    }
}
