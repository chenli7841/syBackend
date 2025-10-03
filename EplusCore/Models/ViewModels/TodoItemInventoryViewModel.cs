using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels
{
    public class TodoItemInventoryViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Comment { get; set; }
        public string Resolution { get; set; }
        public string OrderInfo { get; set; }
        public string CustomerInfo { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateResolved { get; set; }
        public string DateCreatedDisplay => DateCreated.ToString("yyyy-MM-dd HH:mm:ss");
        public string DateResolvedDisplay => DateResolved?.ToString("yyyy-MM-dd HH:mm:ss");
        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public IList<TodoItemAssigneeEntity> Assignees { get; set; }
        public IList<TodoItemCustomerEntity> Customers { get; set; }
        public IList<TodoItemOrderEntity> Orders { get; set; }
        public bool CanUpdate { get; set; } = false;
        public bool CanComplete { get; set; } = false;
        public TodoItemStatusType Status { get; set; }
        public string StatusText { get; set; }
    }
}
