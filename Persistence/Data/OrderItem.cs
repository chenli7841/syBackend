using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int? Quantity { get; set; }
        public int? OrderBaggageId { get; set; }

        public virtual Item Item { get; set; }
        public virtual TransportOrder Order { get; set; }
    }
}
