using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;

namespace WebUI.Models.ViewModels
{
    public class OrderDraftViewModel
    {
        public OrderDraftViewModel()
        {
            Users = new List<UserEntity>();
        }

        public string DomesticCarrier { get; set; }
        public string DomesticNumber { get; set; }
        public int RecipientId { get; set; }
        public IEnumerable<UserEntity> Users { get; set; }
        public int RouteId { get; set; }
        public IEnumerable<RouteEntity> Routes { get; set; }
        public OrderState OrderState { get; set; }
        public int PickupLocationId { get; set; }
        public IEnumerable<PickUpLocationEntity> PickupLocations { get; set; }
    }
}
