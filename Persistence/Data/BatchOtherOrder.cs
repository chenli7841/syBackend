using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BatchOtherOrder
    {
        public int BatchId { get; set; }
        public string OtherOrder { get; set; }

        public virtual Batch Batch { get; set; }
    }
}
