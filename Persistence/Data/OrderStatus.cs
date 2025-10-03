using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderStatus
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserId { get; set; }

        public virtual TransportOrder Order { get; set; }
        public virtual User User { get; set; }
    }
}
