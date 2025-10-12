using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Company
    {
        public Company()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<TransportOrder> TransportOrders { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }

        public virtual ICollection<PickUpLocation> PickUpLocations { get; set; }
    }
}
