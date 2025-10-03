using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderBaggage
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal WeightKg { get; set; }

        public virtual TransportOrder Order { get; set; }
    }
}
