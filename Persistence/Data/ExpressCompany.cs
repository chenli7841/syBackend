using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ExpressCompany
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int OrderNum { get; set; }
        public ulong IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public ulong? IsShow { get; set; }
    }
}
