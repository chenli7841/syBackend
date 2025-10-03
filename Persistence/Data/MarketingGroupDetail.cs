using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class MarketingGroupDetail
    {
        public long Id { get; set; }
        public ulong? IsDel { get; set; }
        public string ActivityName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal GoodsPrice { get; set; }
        public decimal RebateMoney { get; set; }
        public int AllNumber { get; set; }
        public int WinNumber { get; set; }
        public int? JoinNumber { get; set; }
        public bool? ActivityStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public long GoodsId { get; set; }
        public string GoodsPicture { get; set; }
        public string GoodsName { get; set; }
        public ulong? IsSuccess { get; set; }
        public long ShopId { get; set; }
    }
}
