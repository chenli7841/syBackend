using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class QiniuWatermark
    {
        public long Id { get; set; }
        public ulong? WmImageOpen { get; set; }
        public string WmImageId { get; set; }
        public int? WmImageAlpha { get; set; }
        public string WmImagePos { get; set; }
        public ulong? WmTextOpen { get; set; }
        public string WmText { get; set; }
        public int? WmTextFontSize { get; set; }
        public string WmTextPos { get; set; }
        public string WmTextColor { get; set; }
        public string WmTextFont { get; set; }
        public long? ShopId { get; set; }
        public long? UserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
