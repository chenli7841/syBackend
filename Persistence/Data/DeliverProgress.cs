using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class DeliverProgress
    {
        public DeliverProgress()
        {
            Batches = new HashSet<Batch>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Percent { get; set; }
        public bool Hide { get; set; }
        public int Sequence { get; set; }
        public bool IsMain { get; set; }
        public string Description { get; set; }
        public int RouteId { get; set; }

        public virtual Route Route { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
    }
}
