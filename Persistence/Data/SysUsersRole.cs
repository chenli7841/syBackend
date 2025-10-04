using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysUsersRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string RoleCode { get; set; }
    }
}
