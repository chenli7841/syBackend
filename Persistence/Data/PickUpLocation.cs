using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class PickUpLocation
    {
        public PickUpLocation()
        {
            TransportOrders = new HashSet<TransportOrder>();
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Name { get; set; }
        public bool Type { get; set; }
        public int? BelongsToId { get; set; }
        public string DetailArea { get; set; }
        public string LatAndLng { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public bool Category { get; set; }
        public decimal DistrictAdditionalCost { get; set; }
        public decimal StorageCost { get; set; }
        public int? Number { get; set; }
        public int? AreaId { get; set; }
        public bool Visible { get; set; }
        public int Version { get; set; }
        public string Note { get; set; }
        public int? CompanyId { get; set; }

        public virtual User BelongsTo { get; set; }
        public virtual ICollection<TransportOrder> TransportOrders { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual Company Company { get; set; }
    }
}
