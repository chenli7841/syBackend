using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysOwnerAudit
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public bool AuditState { get; set; }
        public string AuditContent { get; set; }
        public long? UserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
