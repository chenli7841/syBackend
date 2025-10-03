using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using RingCentral;
using User = Persistence.Data.User;

namespace Persistence.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;

        public TodoItemService(
            EplusDbContext context,
            IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<PagedResult<TodoItemEntity>> ListAsync(TodoItemListFilterOptions filterOptions)
        {
            var todos = _context.TodoItem
                .Include(t => t.TodoItemAssignees).ThenInclude(a => a.Assignee).ThenInclude(u => u.Customer)
                .Where(o =>
                    (!filterOptions.CreatedByUserId.HasValue || o.CreatedByUserId == filterOptions.CreatedByUserId.Value) &&
                    (!filterOptions.AssigneeUserId.HasValue || o.TodoItemAssignees.Any(a => a.UserId == filterOptions.AssigneeUserId))
                ) 
                .Include(t => t.CreatedBy).ThenInclude(u => u.Customer)
                .Select(t => new TodoItem
                {
                    Id = t.Id,
                    Comment = t.Comment,
                    Message = t.Message,
                    CreatedByUserId = t.CreatedByUserId,
                    DateCreated = t.DateCreated,
                    DateResolved = t.DateResolved,
                    Status = t.Status,
                    Resolution = t.Resolution,
                    NotifyCustomer = t.NotifyCustomer,
                    CreatedBy = new User
                    {
                        Id = t.CreatedBy.Id,
                        Customer = new Customer { Name = t.CreatedBy.Customer.Name },
                        OrderStartNumber = t.CreatedBy.OrderStartNumber
                    },
                    TodoItemAssignees = t.TodoItemAssignees.Select(a => new TodoItemAssignee
                    {
                        Assignee = new User
                        {
                            Id = a.Assignee.Id,
                            OrderStartNumber = a.Assignee.OrderStartNumber,
                            Customer = new Customer { Name = a.Assignee.Customer.Name }
                        }
                    }).ToList(),
                    CustomerInfo = t.CustomerInfo,
                    OrderInfo = t.OrderInfo
                })
                .OrderBy(o => o.Status)
                .ThenByDescending(o => o.DateCreated);

            var total = await todos.CountAsync();
            var pagedOrders = todos.Skip(filterOptions.Skip).Take(filterOptions.PageSize);
            var items = await pagedOrders.Select(
                o => _mapper.Map<TodoItemEntity>(o)).ToListAsync();

            var result = new PagedResult<TodoItemEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }

        public async Task CreateAsync(int createdByUserId, string customerInfo, string message, string comment, string orderInfo, int[] assigneeUserIds)
        {
            var item = new TodoItem
            {
                DateCreated = DateTime.Now,
                CreatedByUserId = createdByUserId,
                Message = message,
                Comment = comment,
                CustomerInfo = customerInfo,
                OrderInfo = orderInfo,
                Status = (int)TodoItemStatusType.PendingProcess,
            };
            foreach (int id in assigneeUserIds)
            {
                item.TodoItemAssignees.Add(new TodoItemAssignee
                {
                    UserId = id
                });
            }

            await _context.TodoItem.AddAsync(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, string resolution, bool notifyCustomer, DateTime dateResolved)
        {
            var todo = await _context.TodoItem.Include(i => i.TodoItemCustomers).ThenInclude(c => c.Customer).FirstOrDefaultAsync(i => i.Id == id);
            if (todo == null)
            {
                throw new ArgumentException($"待办事项 {id} 不存在.");
            }
            todo.NotifyCustomer = notifyCustomer;
            todo.Resolution = resolution;
            todo.Status = (int)TodoItemStatusType.Processed;
            todo.DateResolved = dateResolved;
            await _context.SaveChangesAsync();

            if (notifyCustomer && todo.TodoItemCustomers.Count > 0)
            {
                foreach (var c in todo.TodoItemCustomers)
                {
                    if (!string.IsNullOrWhiteSpace(c.Customer.Mailbox))
                    {
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential("notification.eplus@gmail.com", "dybqcagazakncdqb"),
                            EnableSsl = true
                        };

                        var mail = new MailMessage();
                        mail.From = new MailAddress("notification.eplus@gmail.com");
                        mail.To.Add(c.Customer.Mailbox);
                        mail.Subject = MessageUtils.TodoItemCompleteSubject;
                        mail.Body = todo.Resolution;
                        mail.IsBodyHtml = true;
                        smtpClient.Send(mail);
                    }
                }

            }
        }

        public async Task UpdateStatusAsync(int id, TodoItemStatusType status)
        {
            var todo = await _context.TodoItem.FirstOrDefaultAsync(i => i.Id == id);
            if (todo == null)
            {
                throw new ArgumentException($"待办事项 {id} 不存在.");
            }
            todo.Status = (int)status;
            await _context.SaveChangesAsync();
        }

        public async Task DeteteAsync(int id)
        {
            var todo = await _context.TodoItem.FirstOrDefaultAsync(i => i.Id == id);
            if (todo != null)
            {
                _context.TodoItem.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
