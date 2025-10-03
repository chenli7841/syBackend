using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Area
    {
        public ulong Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public int CountryCode { get; set; }
    }
}
