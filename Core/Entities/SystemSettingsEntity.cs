namespace Domain.Entities
{
    public class SystemSettingsEntity
    {
        public string SchedulePickUpText { get; set; }
        public decimal DeliveryInitialCost { get; set; }
        public decimal DeliveryRenewalCost { get; set; }
        public decimal DeliveryLargeHandlingCost { get; set; }
        public decimal DeliveryOversizedHandlingCost { get; set; }
        public decimal DeliveryDistanceAdditionalCost { get; set; }
        public decimal CostStorageTimeout { get; set; }
        public decimal WeChatServiceChat { get; set; }
    }

    public class SystemPhotoEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
