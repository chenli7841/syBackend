using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace WebUI.Models.ViewModels
{
    public class OrderInventoryViewModel
    {
        public OrderInventoryViewModel()
        {
            Batches = new List<BatchViewModel>();
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string DomesticNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public OrderStatusViewModel LatestStatus { get; set; }
        public string Memo { get; set; }
        public string Group { get; set; }
        public UserEntity Creator { get; set; }
        public IList<BatchViewModel> Batches { get; set; }
        public string StateText { get; set; }
        public string CargoNumber { get; set; }
        public int? BaggageCount { get; set; }
        public bool HasPaid { get; set; }

        public string DeliveryBatch
        {
            get
            {
                return Batches.FirstOrDefault(b => b.IsInDeliveryStage)?.Name;
            }
        }

        public string DateCreatedDisplay => DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
