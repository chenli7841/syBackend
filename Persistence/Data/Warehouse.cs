using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Batches = new HashSet<Batch>();
            Routes = new HashSet<Route>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Photo { get; set; }
        public int? DisplaySequence { get; set; }
        public int CompanyId { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<BatchPallet> BatchPallets { get; set; }
        public virtual ICollection<BatchWarehouseReceive> BatchWarehouseReceives { get; set; }
        public virtual ICollection<LoadDeliveryBatch> LoadDeliveryBatches { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual Company Company { get; set; }
    }
}
