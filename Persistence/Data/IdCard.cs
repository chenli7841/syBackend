using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class IdCard
    {
        public IdCard()
        {
            IntegrationUsers = new HashSet<IntegrationUser>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string FrontUrl { get; set; }
        public string BackUrl { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual ICollection<IntegrationUser> IntegrationUsers { get; set; }
    }
}
