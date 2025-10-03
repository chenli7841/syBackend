using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Item
    {
        public Item()
        {
            ItemPhotos = new HashSet<ItemPhoto>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Brand { get; set; }
        public string Upc { get; set; }
        public decimal? ClaimPrice { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? Weight { get; set; }
        public string Unit { get; set; }
        public int? CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public string Count { get; set; }
        public string EnglishName { get; set; }
        public string EnglishBrand { get; set; }
        public string EnglishFormat { get; set; }
        public string EnglishUnit { get; set; }
        public string EnglishCount { get; set; }
        public string HsCode { get; set; }
        public string EnglishType { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
        public decimal? Point { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ItemPhoto> ItemPhotos { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
