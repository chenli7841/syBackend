using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysInvoiceQualification
    {
        public long Id { get; set; }
        public string EntityName { get; set; }
        public string TaxpayersNum { get; set; }
        public string RegisterAddress { get; set; }
        public string RegisterPhone { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public long? AppUserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
        public ulong? IsDefault { get; set; }
        public bool? IsAuditState { get; set; }
    }
}
