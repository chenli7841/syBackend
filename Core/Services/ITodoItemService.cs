using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ITodoItemService
    {
        Task<PagedResult<TodoItemEntity>> ListAsync(TodoItemListFilterOptions filterOptions);
        Task CreateAsync(int createdByUserId, string customerInfo, string message, string comment, string orderInfo, int[] assigneeUserIds);
        Task UpdateAsync(int id, string resolution, bool notifyCustomer, DateTime dateResolved);
        Task UpdateStatusAsync(int id, TodoItemStatusType status);
        Task DeteteAsync(int id);
    }
}
