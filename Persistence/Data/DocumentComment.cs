using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class DocumentComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int DocumentId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Document Document { get; set; }
    }
}
