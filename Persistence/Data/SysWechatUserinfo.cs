using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysWechatUserinfo
    {
        public long Id { get; set; }
        public long? AppUserId { get; set; }
        public string Phone { get; set; }
        public string Openid { get; set; }
        public string Nickname { get; set; }
        public bool? Sex { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Headimgurl { get; set; }
        public string Privilege { get; set; }
        public string Unionid { get; set; }
    }
}
