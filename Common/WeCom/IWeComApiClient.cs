using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Common.WeCom
{
    public interface IWeComApiClient
    {
        Task<WeComConnectionResult> TestConnectionAsync(CancellationToken cancellationToken = default);
        Task<WeComMassMessageResult> CreateMassMessageAsync(WeComMassMessageRequest request, CancellationToken cancellationToken = default);
        Task<IList<WeComCustomerGroup>> GetCustomerGroupsAsync(string ownerUserId = null, CancellationToken cancellationToken = default);
        Task<WeComCustomerGroup> GetCustomerGroupAsync(string chatId, CancellationToken cancellationToken = default);
    }

    public class WeComMassMessageRequest
    {
        public string ChatType { get; set; } = "single";
        public string SenderUserId { get; set; }
        public bool AllowSelect { get; set; }
        public IList<string> ExternalUserIds { get; set; } = new List<string>();
        public IList<string> ChatIds { get; set; } = new List<string>();
        public string Text { get; set; }
        public IList<WeComMessageAttachment> Attachments { get; set; } = new List<WeComMessageAttachment>();
    }

    public class WeComMessageAttachment
    {
        public string MessageType { get; set; }
        public string MediaId { get; set; }
        public string PictureUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string AppId { get; set; }
        public string Page { get; set; }
    }

    public class WeComMassMessageResult
    {
        public string MessageId { get; set; }
        public IList<string> FailedRecipientIds { get; set; } = new List<string>();
    }

    public class WeComConnectionResult
    {
        public bool Connected { get; set; }
        public int AvailableSenderCount { get; set; }
    }

    public class WeComCustomerGroup
    {
        public string ChatId { get; set; }
        public string Name { get; set; }
        public string OwnerUserId { get; set; }
        public int Status { get; set; }
        public int MemberCount { get; set; }
    }

}
