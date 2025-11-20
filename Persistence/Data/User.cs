using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class User
    {
        public User()
        {
            BalanceHistoryFromUsers = new HashSet<BalanceHistory>();
            BalanceHistoryToUsers = new HashSet<BalanceHistory>();
            BannedUserRoutes = new HashSet<BannedUserRoute>();
            BatchBelongsToUsers = new HashSet<Batch>();
            BatchRecipientUsers = new HashSet<Batch>();
            BatchUsers = new HashSet<Batch>();
            Customers = new HashSet<Customer>();
            DocumentComments = new HashSet<DocumentComment>();
            DocumentCreatedBies = new HashSet<Document>();
            DocumentModifiedBies = new HashSet<Document>();
            DocumentVisibleUsers = new HashSet<Document>();
            InverseBelongsToNavigation = new HashSet<User>();
            OrderActionHistories = new HashSet<OrderActionHistory>();
            OrderStatuses = new HashSet<OrderStatus>();
            OrderUserActions = new HashSet<OrderUserAction>();
            PickUpLocations = new HashSet<PickUpLocation>();
            TransportOrderCreatedBies = new HashSet<TransportOrder>();
            TransportOrderOwners = new HashSet<TransportOrder>();
            Coupons = new HashSet<Coupon>();
            CouponStatuses = new HashSet<CouponStatus>();
            CouponAssignedUsers = new HashSet<Coupon>();
            CouponConsumedUsers = new HashSet<Coupon>();
            TodoItemAssignees = new HashSet<TodoItemAssignee>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public int CustomerId { get; set; }
        public string OrderStartNumber { get; set; }
        public long? PickUpLocationId { get; set; }
        public int? BelongsToId { get; set; }
        public string CanadaPhoneNumber { get; set; }
        public int Level { get; set; }
        public decimal Balance { get; set; }
        public string Settings { get; set; }
        public decimal ClearingPortCost { get; set; }
        public string WeChat { get; set; }
        public string BelongsTo { get; set; }
        public bool IsUpdated { get; set; }
        //public string PickUpLocation { get; set; }
        public string PostalCode { get; set; }
        public bool IsPending { get; set; }
        public bool IsTestAccount { get; set; }
        public int? DefaultBatchId { get; set; }
        public string PickUpAddress { get; set; }
        public string PickUpPhoneNumber { get; set; }
        public string RoleName { get; set; }
        public string ChinaPhoneNumber { get; set; }
        public string PayPassword { get; set; }
        public decimal AddOnCost { get; set; }
        public decimal Credit { get; set; }
        public decimal StorageCost { get; set; }
        public string Avatar { get; set; }
        public string QrPath { get; set; }
        public string NickName { get; set; }
        public int? DisplaySequence { get; set; }
        public string Description { get; set; }
        public string Mailbox { get; set; }
        public int? CompanyId { get; set; }


        public virtual User BelongsToNavigation { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Batch DefaultBatch { get; set; }
        public virtual PickUpLocation PickUpLocationNavigation { get; set; }
        public virtual ICollection<BalanceHistory> BalanceHistoryFromUsers { get; set; }
        public virtual ICollection<BalanceHistory> BalanceHistoryToUsers { get; set; }
        public virtual ICollection<BannedUserRoute> BannedUserRoutes { get; set; }
        public virtual ICollection<Batch> BatchBelongsToUsers { get; set; }
        public virtual ICollection<Batch> BatchRecipientUsers { get; set; }
        public virtual ICollection<Batch> BatchUsers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<DocumentComment> DocumentComments { get; set; }
        public virtual ICollection<Document> DocumentCreatedBies { get; set; }
        public virtual ICollection<Document> DocumentModifiedBies { get; set; }
        public virtual ICollection<Document> DocumentVisibleUsers { get; set; }
        public virtual ICollection<User> InverseBelongsToNavigation { get; set; }
        public virtual ICollection<OrderActionHistory> OrderActionHistories { get; set; }
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
        public virtual ICollection<OrderStatusInternal> OrderInternalStatuses { get; set; }
        public virtual ICollection<OrderUserAction> OrderUserActions { get; set; }
        public virtual ICollection<PickUpLocation> PickUpLocations { get; set; }
        public virtual ICollection<TransportOrder> TransportOrderCreatedBies { get; set; }
        public virtual ICollection<TransportOrder> TransportOrderOwners { get; set; }
        public virtual ICollection<SupportUser> SupportUsers { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<CouponBatch> CouponBatches { get; set; }
        public virtual ICollection<CouponStatus> CouponStatuses { get; set; }
        public virtual ICollection<Coupon> CouponAssignedUsers { get; set; }
        public virtual ICollection<Coupon> CouponConsumedUsers { get; set; }
        public virtual ICollection<EmailData> EmailDataRecipients { get; set; }
        public virtual ICollection<EmailData> EmailDataSenders { get; set; }
        public virtual ICollection<TodoItemAssignee> TodoItemAssignees { get; set; }
        public virtual ICollection<TodoItemCustomer> TodoItemCustomers { get; set; }
        public virtual ICollection<TodoItem> TodoItems { get; set; }
        public virtual Company Company { get; set; }
        public virtual Role UserRole { get; set; }
    }
}
