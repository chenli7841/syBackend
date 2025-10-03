using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ShopSupplier
    {
        public long Id { get; set; }
        public long AppUserId { get; set; }
        public bool? AuditState { get; set; }
        public string AuditContent { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Contacts { get; set; }
        public string Phone { get; set; }
        public long? AreaId { get; set; }
        public string AreaName { get; set; }
        public string Address { get; set; }
        public string JuridicalPerson { get; set; }
        public string BusinessLicense { get; set; }
        public string OtherCertificate { get; set; }
        public string IdentityCard { get; set; }
        public string IdentityCardFront { get; set; }
        public string IdentityCardBack { get; set; }
        public string CompanyName { get; set; }
        public string CreditCode { get; set; }
        public long? RecommenderId { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal? CommissionProportion { get; set; }
        public ulong IsOperate { get; set; }
    }
}
