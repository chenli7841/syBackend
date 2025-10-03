using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BasePayConfig
    {
        public long Id { get; set; }
        public string PayName { get; set; }
        public string PayMode { get; set; }
        public string PayDescribe { get; set; }
        public string PayAccount { get; set; }
        public string AppId { get; set; }
        public string SignType { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public bool IsEnable { get; set; }
        public string GatewayUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string MchId { get; set; }
    }
}
