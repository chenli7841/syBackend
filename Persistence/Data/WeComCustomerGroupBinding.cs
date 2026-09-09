using System;

#nullable disable

namespace Persistence.Data
{
    public class WeComCustomerGroupBinding
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string ChatId { get; set; }
        public string GroupOwnerUserId { get; set; }
        public string GroupName { get; set; }
        public string BindingSource { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
