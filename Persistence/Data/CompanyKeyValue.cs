using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class CompanyKeyValue
    {
        public CompanyKeyValue()
        {
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public virtual Company Company { get; set; }
    }
}
