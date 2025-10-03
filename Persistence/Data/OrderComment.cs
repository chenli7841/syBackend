using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class OrderComment
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public bool OrderType { get; set; }
        public bool CommentType { get; set; }
        public bool CommentLevel { get; set; }
        public string CommentContent { get; set; }
        public string CommentImages { get; set; }
        public long ClientId { get; set; }
        public long? ShopId { get; set; }
        public long GoodsId { get; set; }
        public long? SkuId { get; set; }
        public bool IsReply { get; set; }
        public string ReplyContent { get; set; }
        public long? ReplyUserId { get; set; }
        public DateTime? ReplyTime { get; set; }
        public bool IsDel { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
