using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class OrderEntity
    {
        public OrderEntity()
        {
            Status = new List<OrderStatusEntity>();
            Baggages = new List<OrderBaggageEntity>();
            Items = new List<OrderItemEntity>();
            Photos = new List<OrderPhotoEntity>();
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string DomesticCarrier { get; set; }
        public string DomesticNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal WeightKg { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal DistrictAdditionalCost { get; set; }
        public string IntNumber { get; set; }
        public string IntCarrier { get; set; }
        public UserEntity Creator { get; set; }
        public decimal Insurance { get; set; }
        public decimal Duty { get; set; }
        public decimal Discount { get; set; }
        public decimal StorageCost { get; set; }
        public decimal WarehouseCost { get; set; }
        public decimal PortMisCost { get; set; }
        public decimal FumigationCost { get; set; }
        public decimal OversizeCost { get; set; }
        public decimal Tax { get; set; }
        public decimal ItemCost { get; set; }
        public string CustomerNotes { get; set; }
        public string WarehouseNotes { get; set; }
        public string ActionReason { get; set; }
        public int? DraftById { get; set; }
        public int? PickUpLocationId { get; set; }
        public OrderState State { get; set; }
        public int? CompanyId { get; set; }
        public decimal? TotalVolume { get; set; }
        public decimal? InsuranceCost { get; set; }
        public IEnumerable<OrderStatusEntity> Status { get; set; }
        public IEnumerable<OrderStatusEntity> InternalStatus { get; set; }
        public IEnumerable<OrderBaggageEntity> Baggages { get; set; }
        public IEnumerable<OrderItemEntity> Items { get; set; }
        public IEnumerable<OrderPhotoEntity> Photos { get; set; }
        public PickUpLocationEntity PickUpLocation { get; set; }

        // TODO: only keep id
        public int? RouteId { get; set; }
        public RouteEntity Route { get; set; }
        public string RecipientAddress { get; set; }
        public OrderScanStatusType ScanStatusType { get; set; }
    }

    public class OrderBaggageEntity
    {
        public int Id { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal WeightKg { get; set; }
    }

    public class OrderStatusEntity
    {
        public OrderStatusType Status { get; set; }
        public DateTime Date { get; set; }
        public UserEntity Operator { get; set; }
    }

    public class OrderItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public decimal ClaimPrice { get; set; }
        public string Material { get; set; }
        public string Category { get; set; }
    }

    public class OrderPhotoEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

    public class DataAnalysisOrderEntity
    {
        public int OrderId { get; set; }
        public string DateCreated { get; set; }
        public string OrderStartNumber { get; set; }
        public string OrderNumber { get; set; }
        public string Route { get; set; }
        public decimal? ShippingCost { get; set; }
        public string PickUpLocation { get; set; }
        public string Agent { get; set; }
        public string ItemType { get; set; }
        public string LoadDeliveryBatchName { get; set; }
        public decimal PayCommission { get; set; }
    }

    public class DataAnalysisOrderSummary
    {
        public string GroupId { get; set; }
        public int OrderCount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal PayCommission { get; set; }
    }
}
