using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsSpecification
    {
        public long Id { get; set; }
        public string SpecName { get; set; }
        public bool? SpecType { get; set; }
        public long? ParentId { get; set; }
        public long? GoodsId { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
