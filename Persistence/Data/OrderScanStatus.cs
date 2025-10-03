using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderScanStatus
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Status { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
    }
}
