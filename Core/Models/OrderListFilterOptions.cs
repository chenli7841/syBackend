using Domain.Enums;

namespace Domain.Models
{
    public class OrderListFilterOptions : FilterOptions
    {
        public string OrderNumberToSearch { get; set; }
        public string DomesticNumberToSearch { get; set; }
        public string CreatorToSearch { get; set; }
        public OrderState? OrderState { get; set; }
    }
}
