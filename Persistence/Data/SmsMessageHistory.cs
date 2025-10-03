using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SmsMessageHistory
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public bool? Type { get; set; }
        public string ToMobile { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
        public string TemplateCode { get; set; }
        public string Title { get; set; }
    }
}
