using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class BaseArea
    {
        public ulong Id { get; set; }
        public string Code { get; set; }
        public string AreaName { get; set; }
        public int ParentId { get; set; }
        public string FirstWord { get; set; }
        public int Level { get; set; }
        public bool AreaStatus { get; set; }
        public bool AreaType { get; set; }
        public string PostalCode { get; set; }
    }
}
