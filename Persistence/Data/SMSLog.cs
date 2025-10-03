using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SMSLog
    {
        public int Id { get; set; }
        public int? BatchId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
