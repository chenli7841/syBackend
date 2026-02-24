using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Batch
    {
        public Batch()
        {
            BalanceHistories = new HashSet<BalanceHistory>();
            BatchBoxes = new HashSet<BatchBox>();
            BatchOrderMaps = new HashSet<BatchOrderMap>();
            BatchOtherOrders = new HashSet<BatchOtherOrder>();
            InverseMasterBatch = new HashSet<Batch>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public int? UserId { get; set; }
        public int? BelongsToUserId { get; set; }
        public int? RecipientUserId { get; set; }
        public int GroupType { get; set; }
        public decimal ClearingPortFee { get; set; }
        public decimal AeroShippingCost { get; set; }
        public string AeroNumber { get; set; }
        public bool IsFromChina { get; set; }
        public string BoxInfo { get; set; }
        public decimal? AddOnCost { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? PaidWeightKg { get; set; }
        public decimal? Cost { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsForCreation { get; set; }
        public string IntNumber { get; set; }
        public string IntCarrier { get; set; }
        public decimal Duty { get; set; }
        public decimal StorageCost { get; set; }
        public decimal Discount { get; set; }
        public int? ProgressId { get; set; }
        public int? MasterBatchId { get; set; }
        public int? WarehouseId { get; set; }
        public int? RouteId { get; set; }
        public int? PickType { get; set; }
        public decimal InsuranceFee { get; set; }
        public decimal HeBaoCost { get; set; }
        public decimal TotalExpense { get; set; }
        public int Stage { get; set; }
        public decimal? TargetWeightKg { get; set; }
        public decimal DistrictAdditionalCost { get; set; }
        public int? RecipientAddressId { get; set; }
        public decimal? DeliveryCost { get; set; }
        public long? PickUpLocationId { get; set; }
        public decimal Commission { get; set; }
        public DateTime? DateEntered { get; set; }
        public string Note { get; set; }
        public int? CompanyId { get; set; }


        public virtual User BelongsToUser { get; set; }
        public virtual Batch MasterBatch { get; set; }
        public virtual DeliverProgress Progress { get; set; }
        public virtual User RecipientUser { get; set; }
        public virtual Route Route { get; set; }
        public virtual PickUpLocation PickUpLocation { get; set; }
        public virtual User User { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<BalanceHistory> BalanceHistories { get; set; }
        public virtual ICollection<BatchBox> BatchBoxes { get; set; }
        public virtual ICollection<BatchOrderMap> BatchOrderMaps { get; set; }
        public virtual ICollection<BatchOtherOrder> BatchOtherOrders { get; set; }
        public virtual ICollection<Batch> InverseMasterBatch { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<LoadDeliveryBatch> LoadDeliveryBatches { get; set; }
        public virtual ICollection<EmailData> EmailDatas { get; set; }
        public virtual ICollection<BatchBoxMap> BatchBoxMaps { get; set; }
        public virtual ICollection<BatchPallet> BatchPallets { get; set; }
        public virtual ICollection<BatchPackage> BatchPackages { get; set; }
        public virtual ICollection<BatchWarehouseReceive> BatchWarehouseReceives { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<TodoItem> TodoItems { get; set; }

    }
}
