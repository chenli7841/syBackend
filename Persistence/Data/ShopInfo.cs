using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ShopInfo
    {
        public long Id { get; set; }
        public string ShopNumber { get; set; }
        public long? AppuserId { get; set; }
        public long? UserId { get; set; }
        public string ShopLogo { get; set; }
        public string ShopBackgroundImage { get; set; }
        public string ShopName { get; set; }
        public long? ShopCategoryId { get; set; }
        public bool? ShopMold { get; set; }
        public string ShopPhone { get; set; }
        public bool? ShopProperty { get; set; }
        public string ShopScope { get; set; }
        public long? ShopAreaId { get; set; }
        public string ShopArea { get; set; }
        public string ShopAddress { get; set; }
        public string ShopLnglat { get; set; }
        public DateTime? ShopEffectiveTime { get; set; }
        public bool? ShopAuditState { get; set; }
        public string ShopAuditContent { get; set; }
        public string ShopkeeperName { get; set; }
        public string ShopkeeperPhone { get; set; }
        public string IdentityCard { get; set; }
        public string IdentityCardFront { get; set; }
        public string IdentityCardBack { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CreditCode { get; set; }
        public string BusinessLicense { get; set; }
        public string AuthorizationCertificate { get; set; }
        public int TotalCommentLevel { get; set; }
        public int Volume { get; set; }
        public float HighPraiseRate { get; set; }
        public ulong IsRecommend { get; set; }
        public ulong IsOperate { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public long? RecommenderId { get; set; }
        public string ShopServices { get; set; }
        public decimal CommissionProportion { get; set; }
        public decimal CautionMoney { get; set; }
        public string QrPath { get; set; }
    }
}
