using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class YoudumallUser
    {
        public long Id { get; set; }
        public decimal? AvailableBalance { get; set; }
        public string Mobile { get; set; }
        public string NickName { get; set; }
        public decimal? ZmhBalance { get; set; }
        public decimal? ZmlBalance { get; set; }
        public string UserLevel { get; set; }
        public string Introid { get; set; }
        public string GameFlag { get; set; }
    }
}
