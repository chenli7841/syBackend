using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class QiniuConfig
    {
        public long Id { get; set; }
        public string AccessKey { get; set; }
        public string Bucket { get; set; }
        public string Host { get; set; }
        public string SecretKey { get; set; }
        public string Type { get; set; }
        public string Zone { get; set; }
    }
}
