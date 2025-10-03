using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderPhoto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual TransportOrder Order { get; set; }
    }
}
