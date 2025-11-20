using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Role
    {
        public Role()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string Code { get; set; }
        public bool IsInternal { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
