using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ShopCategory
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Level { get; set; }
        public bool? IsDisplay { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public bool CategoryType { get; set; }
        public string CategoryDescribe { get; set; }
        public int? SerialNumber { get; set; }
    }
}
