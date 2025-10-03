using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ShopReceiveAddress
    {
        public long Id { get; set; }
        public long ShopId { get; set; }
        public string Consignee { get; set; }
        public string Mobile { get; set; }
        public string AreaCode { get; set; }
        public string DetailArea { get; set; }
        public bool Type { get; set; }
    }
}
