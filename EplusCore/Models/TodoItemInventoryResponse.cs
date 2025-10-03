using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class TodoItemInventoryResponse
    {
        public IEnumerable<UserEntity> Users { get; set; }
        public IEnumerable<UserEntity> AdminUsers { get; set; }
        public bool CanDelete { get; set; }
        public TodoItemInventoryResponse()
        {
            Users = new List<UserEntity>();
            AdminUsers = new List<UserEntity>();
        }
    }
}
