using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class DictDetail
    {
        public long Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string Sort { get; set; }
        public long? DictId { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
