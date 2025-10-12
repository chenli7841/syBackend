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
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? ActualWeightKg { get; set; }
        public int BatchId { get; set; }
        public string Name { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual ICollection<BatchBoxOrderMap> BatchBoxOrderMaps { get; set; }
        public virtual ICollection<BatchBoxMap> BatchBoxMaps { get; set; }
    }
}
