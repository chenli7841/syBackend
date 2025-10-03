using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class TransportOrderAudit
    {
        public long Id { get; set; }
        public bool Type { get; set; }
        public long BusinessId { get; set; }
        public bool? AuditState { get; set; }
        public string AuditContent { get; set; }
        public long? AuditUser { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public string AuditUserName { get; set; }
        public int? AddressId { get; set; }
        public string ApplyContent { get; set; }
    }
}
