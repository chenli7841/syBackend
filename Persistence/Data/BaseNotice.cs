using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BaseNotice
    {
        public long Id { get; set; }
        public bool? Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? Sort { get; set; }
        public long? OperatorId { get; set; }
        public string OperatorName { get; set; }
    }
}
