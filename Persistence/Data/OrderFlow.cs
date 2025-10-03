using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderFlow
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public bool OrderStatus { get; set; }
        public bool IsDel { get; set; }
        public bool? UserType { get; set; }
        public long? UserId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
