using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class MarketingGoodsCategory
    {
        public long Id { get; set; }
        public string ClassName { get; set; }
        public ulong CategoryDisplay { get; set; }
        public int CategoryLevel { get; set; }
        public string IconKey { get; set; }
        public long? ParentId { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CategoryDescribe { get; set; }
        public int? SerialNumber { get; set; }
    }
}
