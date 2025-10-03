using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class RecordExpressTransport
    {
        public long Id { get; set; }
        public string TransName { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
        public string PickUpLocation { get; set; }
        public long DeliveryAreaId { get; set; }
        public decimal FirstValue { get; set; }
        public decimal SecondValue { get; set; }
        public decimal FirstSectionCost { get; set; }
        public decimal SecondSectionCost { get; set; }
        public decimal ThirdSectionCost { get; set; }
        public decimal FirstWeightCost { get; set; }
    }
}
