using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Route
    {
        public Route()
        {
            BannedUserRoutes = new HashSet<BannedUserRoute>();
            Batches = new HashSet<Batch>();
            DeliverProgresses = new HashSet<DeliverProgress>();
            TransportOrders = new HashSet<TransportOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFromChina { get; set; }
        public decimal FixedPrice { get; set; }
        public decimal Type1Price { get; set; }
        public decimal Type2Price { get; set; }
        public decimal Type3Price { get; set; }
        public int Type { get; set; }
        public int? WarehouseId { get; set; }
        public decimal Type4Price { get; set; }
        public string Description { get; set; }
        public string SupportDescription { get; set; }
        public int? DisplaySequence { get; set; }
        public string SupportWechat { get; set; }
        public string Photo { get; set; }

        public bool IsRegular { get; set; }

        public int CompanyId { get; set; }

        public string Destination { get; set; }

        public string Departure { get; set; }
        public bool? NeedInsurance { get; set; }
        public decimal? InsuranceRatio { get; set; }
        public decimal? DutyRate { get; set; }
        public decimal? VolumeWeightRatio { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<BannedUserRoute> BannedUserRoutes { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<DeliverProgress> DeliverProgresses { get; set; }
        public virtual ICollection<TransportOrder> TransportOrders { get; set; }
        public virtual Company Company { get; set; }
    }
}
