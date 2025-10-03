using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ShopAudit
    {
        public long Id { get; set; }
        public bool Type { get; set; }
        public long ShopId { get; set; }
        public bool AuditState { get; set; }
        public string AuditContent { get; set; }
        public long AuditUser { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public string AuditUserName { get; set; }
    }
}
