using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Log
    {
        public long Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }
        public string ExceptionDetail { get; set; }
        public string LogType { get; set; }
        public string Method { get; set; }
        public string Params { get; set; }
        public string RequestIp { get; set; }
        public long? Time { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Browser { get; set; }
    }
}
