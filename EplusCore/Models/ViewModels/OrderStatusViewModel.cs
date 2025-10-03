using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;

namespace WebUI.Models.ViewModels
{
    public class OrderStatusViewModel
    {
        public OrderStatusType Status { get; set; }
        public DateTime Date { get; set; }
        public UserEntity Operator { get; set; }
    }
}
