using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysInterfaceConfig
    {
        public long Id { get; set; }
        public ulong? IsWechatConnection { get; set; }
        public string WechatAppid { get; set; }
        public string WechatAppsecret { get; set; }
        public ulong? IsQqConnection { get; set; }
        public string QqDomain { get; set; }
        public string QqApplicationSecretKey { get; set; }
        public string QqApplicationIdentiy { get; set; }
        public ulong? IsWeiboConnection { get; set; }
        public string WeiboDomain { get; set; }
        public string WeiboApplicationIdentiy { get; set; }
        public string WeiboApplicationSecretKey { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
