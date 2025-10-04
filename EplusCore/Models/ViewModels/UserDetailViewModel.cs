using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;

namespace WebUI.Models.ViewModels
{
    public class UserDetailViewModel
    {
        public string Name { get; set; }
        public string Code { get; init; }
        public decimal UnpaidAmount { get; init; }
        public decimal Balance { get; init; }
        public int RecentCreatedOrders { get; init; }
        public int Id { get; set; }
        public RoleType Role { get; set; }
        public PickUpLocationEntity RegisteredPickUpLocation { get; set; }
        public int? SelectedPickUpLocationId { get; set; }
        public int BelongsToId { get; set; }
        public string CanadaPhoneNumber { get; set; }
        public int Level { get; set; }
        public decimal Credit { get; set; }
        public int? DisplaySequence { get; set; }
        public string Description { get; set; }
        public IList<UserEntity> Agents { get; set; }
        public IList<PickUpLocationEntity> PickUpLocations { get; set; }
        public string UserName { get; set; }
        public List<RoleEntity> Roles { get; set; }
        public List<string> UserRoleCodes { get; set; }
    }
}
