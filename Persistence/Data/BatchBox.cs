using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BatchBox
    {
        public BatchBox()
        {
            BatchBoxOrderMaps = new HashSet<BatchBoxOrderMap>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public int BatchId { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual ICollection<BatchBoxOrderMap> BatchBoxOrderMaps { get; set; }
    }
}
