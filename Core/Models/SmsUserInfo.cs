namespace Domain.Models
{
    public class SmsUserInfo
    {
        public string OrderStartNumber { get; set; }
        public string BelongsToName { get; set; }
        public int Level { get; set; }
        public string FullName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public int OrderCount { get; set; }
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }
    }
}