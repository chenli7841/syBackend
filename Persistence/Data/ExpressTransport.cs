using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ExpressTransport
    {
        public long Id { get; set; }
        public ulong? TransEms { get; set; }
        public string TransEmsInfo { get; set; }
        public ulong TransExpress { get; set; }
        public string TransExpressInfo { get; set; }
        public ulong? TransMail { get; set; }
        public string TransMailInfo { get; set; }
        public string TransName { get; set; }
        public int? TransTime { get; set; }
        public int? TransType { get; set; }
        public int? TransUser { get; set; }
        public long? StoreId { get; set; }
        public decimal? FreePostage { get; set; }
        public int? FreePostageStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
