using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ChatUser
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public bool Type { get; set; }
        public long ShopId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
