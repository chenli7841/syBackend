using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class TodoItemOrder
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }

        public virtual TodoItem TodoItem { get; set; }
        public virtual TransportOrder Order { get; set; }
    }
}
