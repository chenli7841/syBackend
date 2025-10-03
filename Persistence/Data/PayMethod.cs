using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class PayMethod
    {
        public long Id { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Name { get; set; }
        public bool Type { get; set; }
        public decimal ServiceCharge { get; set; }
    }
}
