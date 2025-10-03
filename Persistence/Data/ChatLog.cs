using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ChatLog
    {
        public long Id { get; set; }
        public string ChatId { get; set; }
        public string FromTo { get; set; }
        public string ChatFrom { get; set; }
        public string FromAvatar { get; set; }
        public string ChatTo { get; set; }
        public string ToAvatar { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public bool? MsgType { get; set; }
        public bool? ChatType { get; set; }
        public string Extras { get; set; }
        public ulong IsRead { get; set; }
    }
}
