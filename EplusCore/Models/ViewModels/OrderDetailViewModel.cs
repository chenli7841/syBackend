using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace WebUI.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {
            Baggages = new List<OrderBaggageEntity>();
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string DomesticNumber { get; set; }
        public string SecondTrackNumber { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal WeightKg { get; set; }
        public decimal Duty { get; set; }
        public decimal InsuranceClaim { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public string Memo { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDelivered { get; set; }
        public DateTime DateInWarehouse { get; set; }
        public DateTime DeliveryBatch { get; set; }
        public UserEntity Creator { get; set; }
        public IEnumerable<OrderBaggageEntity> Baggages { get; set; }
        public RouteEntity Route { get; set; }
        public string WarehouseNotes { get; set; }

        public decimal VolumeWeight
        {
            get
            {
                if (!Baggages.Any())
                {
                    return 0;
                }

                var baggage = Baggages.First();
                return Math.Round(100 * baggage.Length * baggage.Width * baggage.Height / 5000) / 100;
            }
        }

        public decimal TotalBaggageWeight
        {
            get
            {
                return Baggages.Sum(b => b.WeightKg);
            }
        }
    }
}
