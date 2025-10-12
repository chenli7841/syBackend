using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;

namespace WebUI.Models.ViewModels
{
    public class RouteViewModel
    {
        public RouteViewModel()
        {
            ItemPrices = new List<RouteItemPrice>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public RouteType Type { get; set; }
        public bool IsDeleted { get; set; }
        public int? WarehouseId { get; set; }
        public WarehouseEntity Warehouse { get; set; }
        public decimal FixedPrice { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoData { get; set; }
        public string SupportWechat { get; set; }
        public string SupportDescription { get; set; }
        public int? DisplaySequence { get; set; }
        public IList<RouteItemPrice> ItemPrices { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
    }
}
