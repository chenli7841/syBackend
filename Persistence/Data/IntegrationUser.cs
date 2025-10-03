using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class IntegrationUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int IdCardId { get; set; }

        public virtual IdCard IdCard { get; set; }
    }
}
