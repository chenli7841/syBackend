using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TodoItemEntity
    {
        public int Id { get; set; }
        public UserEntity CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public TodoItemStatusType Status { get; set; }

        public string Message { get; set; }
        public string Comment { get; set; }
        public string Resolution { get; set; }
        public string OrderInfo { get; set; }
        public string BatchInfo { get; set; }
        public int BatchId { get; set; }
        public string CustomerInfo { get; set; }
        public DateTime? DateResolved { get; set; }
        public bool NotifyCustomer { get; set; }

        public IList<TodoItemAssigneeEntity> Assignees { get; set; }
        public IList<TodoItemCustomerEntity> Customers { get; set; }
        public IList<TodoItemOrderEntity> Orders { get; set; }
    }

    public class TodoItemAssigneeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrderStartNumber { get; set; }
    }

    public class TodoItemCustomerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrderStartNumber { get; set; }
    }
    public class TodoItemOrderEntity
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
    }
}
