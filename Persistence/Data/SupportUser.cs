using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SupportUser
    {
        public int UserId { get; set; }
        public string WeChat { get; set; }
        public string Warehouse { get; set; }
        public virtual User User { get; set; }
    }
}
