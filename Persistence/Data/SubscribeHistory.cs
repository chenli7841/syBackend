using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SubscribeHistory
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
