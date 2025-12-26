using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BaseAdvert
    {
        public long Id { get; set; }
        public int AdType { get; set; }
        public string AdPictureKey { get; set; }
        public bool? ClickType { get; set; }
        public string GoodsId { get; set; }
        public string AdUrl { get; set; }
        public bool? AdPostiton { get; set; }
        public string AdSize { get; set; }
        public int? Sort { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? CompanyId { get; set; }
    }
}
