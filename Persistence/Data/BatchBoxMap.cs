using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BatchBoxMap
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int BoxId { get; set; }

        public virtual BatchBox BatchBox { get; set; }
        public virtual Batch Batch { get; set; }
    }
}
