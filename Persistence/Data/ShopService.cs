using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ShopService
    {
        public long Id { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public string ServiceName { get; set; }
        public string ServiceImages { get; set; }
    }
}
