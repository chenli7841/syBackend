using System.Collections.Generic;

namespace WebUI.Models.ViewModels
{
    public class OrderItemWithIndex
    {
        public int Index { get; set; }
        public OrderItemEditModel Item { get; set; }
        public IEnumerable<string> AllCategories { get; set; }
    }
}
