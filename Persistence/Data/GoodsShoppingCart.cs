using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsShoppingCart
    {
        public long Id { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? GoodsSkuId { get; set; }
        public long? ShopId { get; set; }
        public int? GoodsNumber { get; set; }
        public long? ClientId { get; set; }
        public long? GoodsId { get; set; }
        public string SpecName { get; set; }
        public string GoodsPicture { get; set; }
    }
}
