using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class PendingUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string WeChat { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string BelongsTo { get; set; }
    }
}
