using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ChinaItem
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public decimal? ClaimPrice { get; set; }
        public string Material { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? WeightPound { get; set; }
        public int OrderId { get; set; }
        public string Category { get; set; }
        public int? CategoryId { get; set; }
        public int? OrderBaggageId { get; set; }
        public string PhotoUrl { get; set; }

        public virtual TransportOrder Order { get; set; }
    }
}
