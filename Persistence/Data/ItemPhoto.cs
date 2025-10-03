using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ItemPhoto
    {
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Item Item { get; set; }
    }
}
