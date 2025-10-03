using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsCartShop
    {
        public long Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public long ShopId { get; set; }
        public string ShopName { get; set; }
        public long ClientId { get; set; }
    }
}
