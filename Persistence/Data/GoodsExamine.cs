using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsExamine
    {
        public long Id { get; set; }
        public string GoodsName { get; set; }
        public ulong? ExamineResult { get; set; }
        public string ExamineDetails { get; set; }
        public DateTime? ExamineTime { get; set; }
        public string ExaminePersonnel { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? GoodsId { get; set; }
    }
}
