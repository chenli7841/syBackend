using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ExpressConfig
    {
        public long Id { get; set; }
        public int? ExpressType { get; set; }
        public string ChargeKey { get; set; }
        public string FreeKey { get; set; }
        public string FreeCustomer { get; set; }
        public string ChargeCustomer { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
