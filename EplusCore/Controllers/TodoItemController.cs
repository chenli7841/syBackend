using AutoMapper;
using Common;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Models.DataTableRequest;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class TodoItemController : ControllerBase
    {
        private ITodoItemService _todoItemService;
        private readonly ISystemSession _systemSession;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TodoItemController(ITodoItemService todoItemService, ISystemSession systemSession, ILogger<TodoItemController> logger, IMapper mapper)
        {
            this._todoItemService = todoItemService;
            _systemSession = systemSession;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<IActionResult> LoadData([FromQuery] TodoItemFilterType filterType, DataTableRequestModel requestModel)
        {
            try
            {
                var todos = await _todoItemService.ListAsync(new TodoItemListFilterOptions()
                {
                    CreatedByUserId = filterType == TodoItemFilterType.Created ? _systemSession.CurrentUser.Id : null,
                    AssigneeUserId = filterType == TodoItemFilterType.Assigned ? _systemSession.CurrentUser.Id : null,
                    PageSize = requestModel.Length,
                    Skip = requestModel.Start
                });

                var itemViewModels = todos.Items.Select(i => this._mapper.Map<TodoItemInventoryViewModel>(i));
                var items = new List<TodoItemInventoryViewModel>();
                foreach (var m in itemViewModels)
                {
                    if (m.Assignees.Any(a => a.Id == _systemSession.CurrentUser.Id))
                    {
                        m.CanUpdate = true;
                    }
                    if (m.CreatedByUserId == _systemSession.CurrentUser.Id && m.Status == TodoItemStatusType.Processed)
                    {
                        m.CanComplete = true;
                    }
                    items.Add(m);
                }
                var data = new PagedResult<TodoItemInventoryViewModel>()
                {
                    Total = todos.Total,
                    Items = items
                };

                return new JsonResult(new { draw = requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = data.Items });
            }
            catch(Exception e)
            {
                if (e.Message.Contains("inner exception") && e.InnerException != null)
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.InnerException.Message }));
                }
                else
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.Message }));
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string message, string customerInfo, string comment, string orderInfo, int[] assigneeUserIds)
        {
            try
            {
                await _todoItemService.CreateAsync(_systemSession.CurrentUser.Id, customerInfo, message, comment, orderInfo, assigneeUserIds, null);

                return new JsonResult(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("inner exception") && e.InnerException != null)
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.InnerException.Message }));
                }
                else
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.Message }));
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string resolution, bool notifyCustomer, DateTime dateResolved)
        {
            try
            {
                await _todoItemService.UpdateAsync(id, resolution, notifyCustomer, dateResolved);
                return new JsonResult(new MethodResult<bool>(true));
            }

            catch (Exception e)
            {
                if (e.Message.Contains("inner exception") && e.InnerException != null)
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.InnerException.Message }));
                }
                else
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.Message }));
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, TodoItemStatusType status)
        {
            try
            {
                await _todoItemService.UpdateStatusAsync(id, status);
                return new JsonResult(new MethodResult<bool>(true));
            }

            catch (Exception e)
            {
                if (e.Message.Contains("inner exception") && e.InnerException != null)
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.InnerException.Message }));
                }
                else
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.Message }));
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _todoItemService.DeteteAsync(id);
                return new JsonResult(new MethodResult<bool>(true));
            }

            catch (Exception e)
            {
                if (e.Message.Contains("inner exception") && e.InnerException != null)
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.InnerException.Message }));
                }
                else
                {
                    return new JsonResult(new MethodResult<bool>(new Error() { Text = e.Message }));
                }
            }
        }
    }
}
