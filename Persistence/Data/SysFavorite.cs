using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysFavorite
    {
        public long Id { get; set; }
        public long? FavoriteId { get; set; }
        public int? Type { get; set; }
        public long? AppUserId { get; set; }
        public string AppUserName { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
