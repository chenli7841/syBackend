namespace WebUI.Models.ApiRequest
{
    public class SetCouponUserRequest
    {
        public int CouponId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
    }
}
