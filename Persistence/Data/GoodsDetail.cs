using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class GoodsDetail
    {
        public long Id { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public long GoodsCategoryId { get; set; }
        public long ShopCategoryId { get; set; }
        public string GoodsName { get; set; }
        public string GoodsAdvertisement { get; set; }
        public long? BrandId { get; set; }
        public decimal? GoodsPrice { get; set; }
        public decimal? MarketPrice { get; set; }
        public string GoodsPicture { get; set; }
        public string ProductCode { get; set; }
        public int GoodsStock { get; set; }
        public int? GoodsWarning { get; set; }
        public string MeasurementUnit { get; set; }
        public string GoodsDetails { get; set; }
        public bool? IsShelf { get; set; }
        public string DistributionPhone { get; set; }
        public string DistributionExplain { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public bool? IsRecommend { get; set; }
        public bool? PlatformRecommend { get; set; }
        public bool? IsNew { get; set; }
        public int? SalesVolume { get; set; }
        public int? GoodsSort { get; set; }
        public bool? IsWarning { get; set; }
        public bool? GoodsStatus { get; set; }
        public int? TotalCommentLevel { get; set; }
        public long ShopId { get; set; }
        public string OtherBrand { get; set; }
        public decimal? GoodsVolume { get; set; }
        public decimal? GoodsWeight { get; set; }
        public decimal? HighPraiseRate { get; set; }
        public bool? DeliveryMode { get; set; }
        public bool? FreightBear { get; set; }
        public bool? FreightMode { get; set; }
        public long? TransportId { get; set; }
        public decimal? FixedFreight { get; set; }
        public decimal Integral { get; set; }
        public long? SupplierId { get; set; }
        public bool GoodsType { get; set; }
        public string VideoPicture { get; set; }
        public string Videos { get; set; }
        public int? ShowSales { get; set; }
        public int? AllSales { get; set; }
        public ulong? IsSupportInvoice { get; set; }
        public bool? SpecificationsType { get; set; }
        public string ShopName { get; set; }
    }
}
