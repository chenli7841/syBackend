using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrchardGame
    {
        public long Id { get; set; }
        public int GameLevel { get; set; }
        public bool UserStatus { get; set; }
        public decimal? RebateCost { get; set; }
        public long AppUserId { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
