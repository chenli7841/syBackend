namespace Domain.Models
{
    public class EmailOrderInfo
    {
        public string OrderNumber { get; set; }
        public decimal ShippingCost { get; set; }
        public string PickUpLocation { get; set; }
    }
}