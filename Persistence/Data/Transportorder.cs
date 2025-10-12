using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class TransportOrder
    {
        public TransportOrder()
        {
            BalanceHistories = new HashSet<BalanceHistory>();
            BatchBoxOrderMaps = new HashSet<BatchBoxOrderMap>();
            BatchOrderMaps = new HashSet<BatchOrderMap>();
            ChinaItems = new HashSet<ChinaItem>();
            OrderActionHistories = new HashSet<OrderActionHistory>();
            OrderBaggages = new HashSet<OrderBaggage>();
            OrderItems = new HashSet<OrderItem>();
            OrderPhotos = new HashSet<OrderPhoto>();
            OrderStatuses = new HashSet<OrderStatus>();
            OrderUserActions = new HashSet<OrderUserAction>();
        }

        public int Id { get; set; }
        public string BaggageNumber { get; set; }
        public string OrderNumber { get; set; }
        public string TransferNumber { get; set; }
        public string DomesticNumber { get; set; }
        public string DomesticCarrier { get; set; }
        public string Route { get; set; }
        public int State { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? RecipientId { get; set; }
        public string ClearingPort { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? VolumnCost { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? WeightPound { get; set; }
        public decimal? WeightKg { get; set; }
        public string AeroNumber { get; set; }
        public int? SenderId { get; set; }
        public string Memo { get; set; }
        public decimal? RemoteCost { get; set; }
        public int CreatedById { get; set; }
        public decimal SuggestedCost { get; set; }
        public decimal? DomesticShippingCost { get; set; }
        public decimal? AirportCharge { get; set; }
        public decimal? Tax { get; set; }
        public decimal? AeroShippingCost { get; set; }
        public decimal DisplayCost { get; set; }
        public decimal ActualCost { get; set; }
        public decimal ClearingPortFee { get; set; }
        public bool IsFromChina { get; set; }
        public string SecondTrackNumber { get; set; }
        public string SecondCarrier { get; set; }
        public string HiddenNotes { get; set; }
        public int? RouteId { get; set; }
        public decimal Duty { get; set; }
        public decimal StorageCost { get; set; }
        public decimal WarehouseCost { get; set; }
        public decimal PortMisCost { get; set; }
        public decimal FumigationCost { get; set; }
        public decimal OversizeCost { get; set; }
        public decimal ItemCost { get; set; }
        public int? RecipientAddressId { get; set; }
        public decimal DistrictAdditionalCost { get; set; }
        public string ActionReason { get; set; }
        public string PhotoUrl { get; set; }
        public int AuditStatus { get; set; }
        public string AuditDetails { get; set; }
        public int? OwnerId { get; set; }
        public int? SendAddressId { get; set; }
        public string ReturnNumber { get; set; }
        public string ReturnCarrier { get; set; }
        public int? ReturnAddressId { get; set; }
        public string ReturnUserName { get; set; }
        public long? PickUpLocationId { get; set; }
        public DateTime? ArriveTime { get; set; }
        public bool IsItemCostUpdated { get; set; }
        public string LoadDeliveryBatchName { get; set; }
        public int? LoadDeliveryBatchId { get; set; }
        public string Enclosure { get; set; }
        public int? CompanyId { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User Owner { get; set; }
        public virtual PickUpLocation PickUpLocation { get; set; }
        public virtual Customer Recipient { get; set; }
        public virtual Route RouteNavigation { get; set; }
        public virtual Customer Sender { get; set; }
        public virtual ICollection<BalanceHistory> BalanceHistories { get; set; }
        public virtual ICollection<BatchBoxOrderMap> BatchBoxOrderMaps { get; set; }
        public virtual ICollection<BatchOrderMap> BatchOrderMaps { get; set; }
        public virtual ICollection<ChinaItem> ChinaItems { get; set; }
        public virtual ICollection<OrderActionHistory> OrderActionHistories { get; set; }
        public virtual ICollection<OrderBaggage> OrderBaggages { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderPhoto> OrderPhotos { get; set; }
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
        public virtual ICollection<OrderStatusInternal> OrderInternalStatuses { get; set; }
        public virtual ICollection<OrderUserAction> OrderUserActions { get; set; }
        public virtual ICollection<EmailData> EmailDatas { get; set; }
        public virtual ICollection<EmailData> EmailDataInWarehouses { get; set; }
        public virtual ICollection<TodoItemOrder> TodoItemOrders { get; set; }
        public virtual Company Company { get; set; }
    }
}
