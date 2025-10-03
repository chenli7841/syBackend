using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class TodoItemAssignee
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }

        public virtual TodoItem TodoItem { get; set; }
        public virtual User Assignee { get; set; }
    }
}
