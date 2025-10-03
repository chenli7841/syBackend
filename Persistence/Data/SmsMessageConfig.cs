using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SmsMessageConfig
    {
        public long Id { get; set; }
        public ulong? SmsEnbale { get; set; }
        public string SmsAccesskeyId { get; set; }
        public string SmsSecret { get; set; }
        public string SmsSign { get; set; }
        public string SmsTestPhone { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
