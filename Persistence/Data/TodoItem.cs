using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public partial class TodoItem
    {
        public TodoItem()
        {
            TodoItemAssignees = new HashSet<TodoItemAssignee>();
            TodoItemCustomers = new HashSet<TodoItemCustomer>();
            TodoItemOrders = new HashSet<TodoItemOrder>();
        }

        public int Id { get; set; }
        public string Message { get; set; }
        public string Comment { get; set; }
        public string Resolution { get; set; }
        public string CustomerInfo { get; set; }
        public string OrderInfo { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateResolved { get; set; }
        public bool NotifyCustomer { get; set; }
        public int Status { get; set; }

        public virtual ICollection<TodoItemAssignee> TodoItemAssignees { get; set; }
        public virtual ICollection<TodoItemCustomer> TodoItemCustomers { get; set; }
        public virtual ICollection<TodoItemOrder> TodoItemOrders { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
