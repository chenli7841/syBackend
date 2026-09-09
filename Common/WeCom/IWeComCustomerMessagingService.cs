using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Common.WeCom
{
    public interface IWeComCustomerMessagingService
    {
        Task BindCustomerGroupAsync(int companyId, int userId, string chatId, string groupOwnerUserId, string groupName, string source, CancellationToken cancellationToken = default);
        Task<IList<WeComCustomerMessageTaskResult>> CreateCustomerMessageTasksAsync(int companyId, IEnumerable<int> userIds, string text, IList<WeComMessageAttachment> attachments = null, CancellationToken cancellationToken = default);
        Task<WeComMassMessageResult> CreateCustomerGroupMessageTaskAsync(string senderUserId, IEnumerable<string> chatIds, string text, IList<WeComMessageAttachment> attachments = null, CancellationToken cancellationToken = default);
    }

    public class WeComCustomerMessageTaskResult
    {
        public string SenderUserId { get; set; }
        public string MessageId { get; set; }
        public IList<int> UserIds { get; set; } = new List<int>();
        public IList<string> FailedChatIds { get; set; } = new List<string>();
    }
}
