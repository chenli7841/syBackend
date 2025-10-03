using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BaseBanner
    {
        public long Id { get; set; }
        public string PictureKey { get; set; }
        public string Name { get; set; }
        public bool Type { get; set; }
        public bool BannerType { get; set; }
        public string Url { get; set; }
        public long? GoodsCategoryId { get; set; }
        public int? OrderNum { get; set; }
        public bool? IsShow { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
