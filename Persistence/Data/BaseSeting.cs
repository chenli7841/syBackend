using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BaseSeting
    {
        public long Id { get; set; }
        public string SetKey { get; set; }
        public string SetValue { get; set; }
        public bool ValueType { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
