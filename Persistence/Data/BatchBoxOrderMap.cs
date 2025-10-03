using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BatchBoxOrderMap
    {
        public int OrderId { get; set; }
        public int BatchBoxId { get; set; }

        public virtual BatchBox BatchBox { get; set; }
        public virtual TransportOrder Order { get; set; }
    }
}
