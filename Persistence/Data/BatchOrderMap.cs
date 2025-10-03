using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BatchOrderMap
    {
        public int BatchId { get; set; }
        public int OrderId { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual TransportOrder Order { get; set; }
    }
}
