using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BatchPallet
    {
        public BatchPallet()
        {
        }

        public int Id { get; set; }
        public int BatchId { get; set; }
        public int? WarehouseId { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? WeightKg { get; set; }
        public string CustomName { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
