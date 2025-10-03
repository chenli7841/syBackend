using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BannedUserRoute
    {
        public int UserId { get; set; }
        public int RouteId { get; set; }

        public virtual Route Route { get; set; }
        public virtual User User { get; set; }
    }
}
