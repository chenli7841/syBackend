using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Dict
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
