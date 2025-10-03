using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ExpressTransArea
    {
        public long Id { get; set; }
        public string AreaName { get; set; }
        public int Level { get; set; }
        public int Sequence { get; set; }
        public long? ParentId { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDel { get; set; }
    }
}
