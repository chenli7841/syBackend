using System;

namespace WebUI.Models.ApiRequest
{
    public class SaveCouponBatchRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public decimal? MinimumPrice { get; set; }
    }
}
