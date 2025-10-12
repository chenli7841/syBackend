using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class LoadDeliveryBatch
    {
        public int Id { get; set; }
        public string FlightInfo { get; set; }
        public string CargoNumber { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public int? WarehouseId { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual Warehouse Warehouse { get; set; }

    }
}
