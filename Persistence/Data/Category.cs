using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsDeleted { get; set; }
        public string HsCode { get; set; }
        public string EnglishName { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
