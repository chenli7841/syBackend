using Domain.Enums;

namespace Domain.Models
{
    public class UserListFilterOptions : FilterOptions
    {
        public string CodeToSearch { get; set; }
        public string PhoneToSearch { get; set; }
        public RoleType? RoleToSearch { get; set; }
        public int[] CompanyIds { get; set; }
        public int[] PickUpLocationIds { get; set; }
    }
}
