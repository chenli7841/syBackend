using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class QiniuContent
    {
        public long Id { get; set; }
        public string Bucket { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string ContentKey { get; set; }
        public string Suffix { get; set; }
        public string Category { get; set; }
        public long? AlbumId { get; set; }
    }
}
