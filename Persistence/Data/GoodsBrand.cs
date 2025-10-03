using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsBrand
    {
        public long Id { get; set; }
        public string FirstWord { get; set; }
        public string BrandName { get; set; }
        public ulong BrandDisplay { get; set; }
        public string BrandPictureKey { get; set; }
        public ulong BrandRecommend { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
