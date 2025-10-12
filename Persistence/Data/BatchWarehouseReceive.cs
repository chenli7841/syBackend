using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BatchWarehouseReceive
    {
        public BatchWarehouseReceive()
        {
        }

        public int Id { get; set; }
        public int BatchId { get; set; }
        public int WarehouseId { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
