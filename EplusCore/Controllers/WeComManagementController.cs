using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.WeCom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/wecom")]
    public class WeComManagementController : ControllerBase
    {
        private readonly IWeComApiClient _apiClient;
        private readonly IWeComCustomerMessagingService _messagingService;
        private readonly EplusDbContext _context;

        public WeComManagementController(IWeComApiClient apiClient, IWeComCustomerMessagingService messagingService, EplusDbContext context)
        {
            _apiClient = apiClient;
            _messagingService = messagingService;
            _context = context;
        }

        [HttpGet("status")]
        public async Task<IActionResult> Status(CancellationToken cancellationToken)
        {
            try { return Ok(await _apiClient.TestConnectionAsync(cancellationToken)); }
            catch (Exception exception) { return BadRequest(new { connected = false, error = exception.Message }); }
        }

        [HttpGet("groups")]
        public async Task<IActionResult> Groups([FromQuery] string name = null, [FromQuery] string ownerUserId = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var groups = await _apiClient.GetCustomerGroupsAsync(ownerUserId, cancellationToken);
                if (!string.IsNullOrWhiteSpace(name)) groups = groups.Where(x => (x.Name ?? string.Empty).IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                return Ok(groups.OrderBy(x => x.Name));
            }
            catch (Exception exception) { return BadRequest(new { error = exception.Message }); }
        }

        [HttpGet("groups/suggest/{userId:int}")]
        public async Task<IActionResult> SuggestGroup(int userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            if (user == null) return NotFound(new { error = $"系统客户账号 {userId} 不存在。" });
            try
            {
                var groups = await _apiClient.GetCustomerGroupsAsync(cancellationToken: cancellationToken);
                var customerNumber = user.OrderStartNumber?.Trim();
                var matches = groups.Where(x => !string.IsNullOrWhiteSpace(customerNumber) && (x.Name ?? string.Empty).IndexOf(customerNumber, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                return Ok(new { user.Id, user.UserName, user.OrderStartNumber, matches });
            }
            catch (Exception exception) { return BadRequest(new { error = exception.Message }); }
        }

        [HttpPost("bindings")]
        public async Task<IActionResult> Bind([FromBody] BindCustomerGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var group = await _apiClient.GetCustomerGroupAsync(request.ChatId, cancellationToken);
                await _messagingService.BindCustomerGroupAsync(request.CompanyId, request.UserId, group.ChatId, group.OwnerUserId, group.Name, "admin_confirmed", cancellationToken);
                return Ok(new { bound = true, request.UserId, group.ChatId, group.Name, group.OwnerUserId });
            }
            catch (Exception exception) { return BadRequest(new { bound = false, error = exception.Message }); }
        }

        [HttpGet("bindings")]
        public async Task<IActionResult> Bindings([FromQuery] int companyId, CancellationToken cancellationToken)
        {
            var bindings = await _context.WeComCustomerGroupBindings.AsNoTracking()
                .Where(x => x.CompanyId == companyId)
                .Join(_context.Users.AsNoTracking(), binding => binding.UserId, user => user.Id,
                    (binding, user) => new
                    {
                        binding.Id, binding.CompanyId, binding.UserId, user.UserName, user.OrderStartNumber,
                        binding.ChatId, binding.GroupName, binding.GroupOwnerUserId,
                        binding.IsActive, binding.BindingSource, binding.UpdatedAt
                    })
                .OrderBy(x => x.OrderStartNumber)
                .ToListAsync(cancellationToken);
            return Ok(bindings);
        }

        [HttpPost("test-message")]
        public async Task<IActionResult> TestMessage([FromBody] SendCustomerGroupTestRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _messagingService.CreateCustomerMessageTasksAsync(request.CompanyId, request.UserIds, request.Text, cancellationToken: cancellationToken);
                return Ok(new { created = true, tasks });
            }
            catch (Exception exception) { return BadRequest(new { created = false, error = exception.Message }); }
        }

        public class BindCustomerGroupRequest
        {
            public int CompanyId { get; set; }
            public int UserId { get; set; }
            public string ChatId { get; set; }
        }

        public class SendCustomerGroupTestRequest
        {
            public int CompanyId { get; set; }
            public IList<int> UserIds { get; set; } = new List<int>();
            public string Text { get; set; }
        }
    }
}
