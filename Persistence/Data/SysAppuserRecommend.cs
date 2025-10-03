using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysAppuserRecommend
    {
        public long Id { get; set; }
        public long AppUserId { get; set; }
        public long? ParentUserId { get; set; }
        public string InviteCode { get; set; }
        public string InviteUrl { get; set; }
        public string QrPath { get; set; }
        public ulong IsDel { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
