using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Document
    {
        public Document()
        {
            DocumentComments = new HashSet<DocumentComment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedById { get; set; }
        public DateTime DateModified { get; set; }
        public int ModifiedById { get; set; }
        public bool PinToTop { get; set; }
        public int? VisibleUserId { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual User VisibleUser { get; set; }
        public virtual ICollection<DocumentComment> DocumentComments { get; set; }
    }
}
