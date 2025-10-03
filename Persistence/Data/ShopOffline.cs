using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ShopOffline
    {
        public long Id { get; set; }
        public long AppUserId { get; set; }
        public bool? AuditState { get; set; }
        public string AuditContent { get; set; }
        public ulong IsOperate { get; set; }
        public string ShopNumber { get; set; }
        public string ShopLogo { get; set; }
        public string ShopName { get; set; }
        public string ShopContacts { get; set; }
        public string ShopPhone { get; set; }
        public bool ShopProperty { get; set; }
        public long? ShopCategoryId { get; set; }
        public long? AreaId { get; set; }
        public string AreaName { get; set; }
        public string ShopAddress { get; set; }
        public string JuridicalPerson { get; set; }
        public string BusinessLicense { get; set; }
        public string OtherCertificate { get; set; }
        public string IdentityCard { get; set; }
        public string IdentityCardFront { get; set; }
        public string IdentityCardBack { get; set; }
        public string CompanyName { get; set; }
        public string CreditCode { get; set; }
        public DateTime CreateTime { get; set; }
        public bool? OperateAuditState { get; set; }
        public string OfflineDetails { get; set; }
        public string OperateTime { get; set; }
        public string UpperPictures { get; set; }
        public string LowerPictures { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string QrPath { get; set; }
        public bool? IsRecommend { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }
}
