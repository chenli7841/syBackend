using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.WeCom;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Services
{
    public class WeComCustomerMessagingService : IWeComCustomerMessagingService
    {
        private readonly EplusDbContext _context;
        private readonly IWeComApiClient _apiClient;

        public WeComCustomerMessagingService(EplusDbContext context, IWeComApiClient apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        public async Task BindCustomerGroupAsync(int companyId, int userId, string chatId, string groupOwnerUserId, string groupName, string source, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(chatId)) throw new ArgumentException("客户群 chat_id 不能为空。", nameof(chatId));
            if (string.IsNullOrWhiteSpace(groupOwnerUserId)) throw new ArgumentException("群主 userid 不能为空。", nameof(groupOwnerUserId));
            if (!await _context.Users.AnyAsync(x => x.Id == userId && x.CompanyId == companyId, cancellationToken)) throw new InvalidOperationException($"系统客户账号 {userId} 不存在或不属于公司 {companyId}。");

            var conflict = await _context.WeComCustomerGroupBindings.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.ChatId == chatId && x.UserId != userId && x.IsActive, cancellationToken);
            if (conflict != null) throw new InvalidOperationException($"该企业微信群已绑定系统客户账号 {conflict.UserId}。");

            var binding = await _context.WeComCustomerGroupBindings.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.UserId == userId, cancellationToken);
            var now = DateTime.UtcNow;
            if (binding == null)
            {
                binding = new WeComCustomerGroupBinding { CompanyId = companyId, UserId = userId, CreatedAt = now };
                await _context.WeComCustomerGroupBindings.AddAsync(binding, cancellationToken);
            }
            binding.ChatId = chatId;
            binding.GroupOwnerUserId = groupOwnerUserId;
            binding.GroupName = groupName;
            binding.BindingSource = string.IsNullOrWhiteSpace(source) ? "manual" : source;
            binding.IsActive = true;
            binding.UpdatedAt = now;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<WeComCustomerMessageTaskResult>> CreateCustomerMessageTasksAsync(int companyId, IEnumerable<int> userIds, string text, IList<WeComMessageAttachment> attachments = null, CancellationToken cancellationToken = default)
        {
            var ids = userIds?.Distinct().ToList() ?? new List<int>();
            if (ids.Count == 0) throw new ArgumentException("至少指定一个系统客户账号 User.Id。", nameof(userIds));
            var bindings = await _context.WeComCustomerGroupBindings.Where(x => x.CompanyId == companyId && x.IsActive && ids.Contains(x.UserId)).ToListAsync(cancellationToken);
            var missing = ids.Except(bindings.Select(x => x.UserId)).ToList();
            if (missing.Count > 0) throw new InvalidOperationException($"以下系统客户尚未绑定专属企业微信群：{string.Join(",", missing)}");

            var results = new List<WeComCustomerMessageTaskResult>();
            foreach (var group in bindings.GroupBy(x => x.GroupOwnerUserId))
            {
                var apiResult = await CreateCustomerGroupMessageTaskAsync(group.Key, group.Select(x => x.ChatId), text, attachments, cancellationToken);
                results.Add(new WeComCustomerMessageTaskResult
                {
                    SenderUserId = group.Key,
                    MessageId = apiResult.MessageId,
                    UserIds = group.Select(x => x.UserId).ToList(),
                    FailedChatIds = apiResult.FailedRecipientIds
                });
            }
            return results;
        }

        public Task<WeComMassMessageResult> CreateCustomerGroupMessageTaskAsync(string senderUserId, IEnumerable<string> chatIds, string text, IList<WeComMessageAttachment> attachments = null, CancellationToken cancellationToken = default)
        {
            return _apiClient.CreateMassMessageAsync(new WeComMassMessageRequest
            {
                ChatType = "group", SenderUserId = senderUserId, AllowSelect = false,
                ChatIds = chatIds?.Distinct().ToList() ?? new List<string>(), Text = text,
                Attachments = attachments ?? new List<WeComMessageAttachment>()
            }, cancellationToken);
        }
    }
}
