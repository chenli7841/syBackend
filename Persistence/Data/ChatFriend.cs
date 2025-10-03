using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ChatFriend
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long? UserShopId { get; set; }
        public bool UserType { get; set; }
        public long FriendId { get; set; }
        public long? FriendShopId { get; set; }
        public bool FriendType { get; set; }
    }
}
