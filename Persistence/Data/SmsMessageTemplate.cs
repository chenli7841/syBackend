using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SmsMessageTemplate
    {
        public long Id { get; set; }
        public bool? Classification { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Type { get; set; }
        public string Content { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public ulong EnabledMessage { get; set; }
        public ulong EnabledNews { get; set; }
    }
}
