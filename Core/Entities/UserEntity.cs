using Domain.Enums;

namespace Domain.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public RoleType Role { get; set; }
        public string UserName { get; set; }
        public string CanadaPhoneNumber { get; set; }
        public string OrderStartNumber { get; set; }
        public string WeChat { get; set; }
        public string Code { get; set; }
        public decimal Balance { get; set; }
        public UserEntity BelongsTo { get; set; }
        public decimal Credit { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public PickUpLocationEntity RegisteredPickUpLocation { get; set; }
        public PickUpLocationEntity SelectedPickUpLocation { get; set; }
        public ShippingAddressEntity ShippingAddress { get; set; }
        public int? SelectedPickUpLocationId { get; set; }
        public int?  DisplaySequence { get; set; }
        public string Description { get; set; }
        public string Mailbox { get; set; }
        
        // TODO: remove this
        public int CustomerId { get; set; }
        public int? BelongsToId { get; set; }
    }

    public class UserRoute
    {
        public RouteEntity Route { get; set; }
        public bool Allowed { get; set; }
    }

    public class PickUpLocationEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public string Phone { get; set; }
        public string DetailArea { get; set; }
        public bool IsSpecial { get; set; }
        public string LatAndLng { get; set; }
        public UserEntity Owner { get; set; }
        public decimal DistrictAdditionalCost { get; set; }
        public decimal StorageCost { get; set; }
        public bool IsDel { get; set; }
        public int NumberOfUsers { get; set; }
        public decimal RecentItemTotalWeightKg { get; set; }
        public int AreaId { get; set; }
        public int Version { get; set; }
        public string Note { get; set; }
    }

    public class PickUpLocationAreaEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class PickUpLocationStatistics
    {
        public int LocationId { get; set; }
        public int NumberOfUsers { get; set; }
        public decimal RecentItemTotalWeightKg { get; set; }
    }

    public class ShippingAddressEntity
    {
        public long Id { get; set; }
        public string Mobile { get; set; }
        public string DetailArea { get; set; }
        public string WeChat { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
    }

    public class SupportUserEntity
    {
        public int UserId { get; set; }
        public string WeChat { get; set; }
        public string Warehouse { get; set; }
    }
}
