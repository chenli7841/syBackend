using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;

namespace WebUI.Models.ViewModels
{
    public class BatchViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public BatchStageType Stage { get; set; }
        public string StageDescription { get; set; }
        public IEnumerable<OrderEntity> Orders { get; set; }
        public BatchViewModel MasterBatch { get; set; }
        public DeliverProgressEntity Progress { get; set; }
        
        public string IntNumber { get; set; }
        public string IntCarrier { get; set; }
        public decimal Cost { get; set; }
        public decimal AddOnCost { get; set; }
        public decimal Duty { get; set; }
        public decimal StorageCost { get; set; }
        public decimal Discount { get; set; }
        public decimal InsuranceFee { get; set; }
        public decimal HeBaoCost { get; set; }
        public decimal WeightKg { get; set; }
        public decimal TotalExpense { get; set; }

        // TODO: only keep Id
        public int? RecipientId { get; set; }
        public UserEntity Recipient { get; set; }
        public int? AgentId { get; set; }
        public UserEntity Agent { get; set; }

        public string DateCreatedDisplay => DateCreated.ToString("yy/MM/dd HH:mm"); 
        public int TotalOrders => Orders.Count();
        public decimal TotalWeightKg => Orders.Sum(o => o.WeightKg);
        public decimal TotalOrderShippingCost => Orders.Sum(o => o.ShippingCost);
        public decimal TotalShippingCost => TotalOrderShippingCost + Duty + StorageCost - Discount;
        public int RecipientMismatchCount => 0; // Orders.Count(o => o.Creator.Id != RecipientId);
        public int AgentMismatchCount => 0; // Orders.Count(o => o.Creator.BelongsTo?.Id != AgentId);
        //public int RouteMismatchCount => Orders.Count(o => o.rou)
        public bool IsInDeliveryStage = false;
        public string FlightInfo { get; set; }
        public string CargoNumber { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string ArrivalTimeDisplay => ArrivalTime?.ToString("yy/MM/dd HH:mm");
    }
}
