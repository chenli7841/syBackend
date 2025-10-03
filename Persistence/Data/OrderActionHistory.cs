using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderActionHistory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public virtual TransportOrder Order { get; set; }
        public virtual User User { get; set; }
    }
}
