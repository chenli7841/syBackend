using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class ToolsGetui
    {
        public long Id { get; set; }
        public string AppId { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string MasterSecret { get; set; }
        public string Http { get; set; }
        public string Https { get; set; }
    }
}
