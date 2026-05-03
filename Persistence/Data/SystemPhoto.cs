using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SystemPhoto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
        public int CompanyId { get; set; }
    }
}
