using System.Collections.Generic;
using System.Linq;
using Domain.Enums;

namespace Domain.Entities
{
    public class RouteEntity
    {
        public static readonly IList<string> Items = new List<string>()
        {
            "书籍", "鞋", "床上用品", "生活用品", "衣物", "学习用品", "运动器材", "玩具", "厨房用品", "户外用品", "配饰", "宠物用品", "汽车配件", "工具", "包，配饰", "电器100以下", "电器100以上", "食品", "保健品，药品", "化妆品"
        };

        public RouteEntity()
        {
            ItemPrices = Items.Select(it => new RouteItemPrice() {Item = it}).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public RouteType Type { get; set; }
        public bool IsDeleted { get; set; }
        public int? WarehouseId { get; set; }
        public WarehouseEntity Warehouse { get; set; }
        public decimal FixedPrice { get; set; }
        public string Photo { get; set; }
        public string SupportWechat { get; set; }
        public string SupportDescription { get; set; }
        public int? DisplaySequence { get; set; }
        public bool IsRegular { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public IList<RouteItemPrice> ItemPrices;
    }

    public class RouteItemPrice
    {
        public string Item { get; set; }
        public decimal Price { get; set; }
    }
}
