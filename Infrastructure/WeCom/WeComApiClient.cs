using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.WeCom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.WeCom
{
    public class WeComApiClient : IWeComApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly WeComOptions _options;
        private readonly SemaphoreSlim _tokenLock = new SemaphoreSlim(1, 1);
        private string _accessToken;
        private DateTime _accessTokenExpiresAtUtc;

        public WeComApiClient(IHttpClientFactory httpClientFactory, WeComOptions options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }

        public async Task<WeComConnectionResult> TestConnectionAsync(CancellationToken cancellationToken = default)
        {
            EnsureConfigured();
            var token = await GetAccessTokenAsync(cancellationToken);
            var json = await GetAsync($"/cgi-bin/externalcontact/get_follow_user_list?access_token={Uri.EscapeDataString(token)}", cancellationToken);
            EnsureSuccess(json);
            return new WeComConnectionResult { Connected = true, AvailableSenderCount = json["follow_user"]?.Count() ?? 0 };
        }

        public async Task<WeComMassMessageResult> CreateMassMessageAsync(WeComMassMessageRequest request, CancellationToken cancellationToken = default)
        {
            EnsureConfigured();
            Validate(request);
            var payload = new JObject
            {
                ["chat_type"] = request.ChatType,
                ["sender"] = request.SenderUserId,
                ["allow_select"] = request.AllowSelect
            };
            payload[request.ChatType == "single" ? "external_userid" : "chat_id_list"] =
                JArray.FromObject((request.ChatType == "single" ? request.ExternalUserIds : request.ChatIds).Distinct());

            if (!string.IsNullOrWhiteSpace(request.Text)) payload["text"] = new JObject { ["content"] = request.Text };
            if (request.Attachments != null && request.Attachments.Count > 0) payload["attachments"] = BuildAttachments(request.Attachments);

            var token = await GetAccessTokenAsync(cancellationToken);
            var json = await PostAsync($"/cgi-bin/externalcontact/add_msg_template?access_token={Uri.EscapeDataString(token)}", payload, cancellationToken);
            EnsureSuccess(json);
            return new WeComMassMessageResult
            {
                MessageId = json.Value<string>("msgid"),
                FailedRecipientIds = json["fail_list"]?.ToObject<List<string>>() ?? new List<string>()
            };
        }

        public async Task<IList<WeComCustomerGroup>> GetCustomerGroupsAsync(string ownerUserId = null, CancellationToken cancellationToken = default)
        {
            EnsureConfigured();
            var token = await GetAccessTokenAsync(cancellationToken);
            var chatIds = new List<string>();
            string cursor = null;
            do
            {
                var payload = new JObject { ["status_filter"] = 0, ["limit"] = 1000 };
                if (!string.IsNullOrWhiteSpace(cursor)) payload["cursor"] = cursor;
                if (!string.IsNullOrWhiteSpace(ownerUserId))
                    payload["owner_filter"] = new JObject { ["userid_list"] = new JArray(ownerUserId) };
                var json = await PostAsync($"/cgi-bin/externalcontact/groupchat/list?access_token={Uri.EscapeDataString(token)}", payload, cancellationToken);
                EnsureSuccess(json);
                chatIds.AddRange(json["group_chat_list"]?.Select(x => x.Value<string>("chat_id")).Where(x => !string.IsNullOrWhiteSpace(x)) ?? Enumerable.Empty<string>());
                cursor = json.Value<string>("next_cursor");
            } while (!string.IsNullOrWhiteSpace(cursor));

            var result = new List<WeComCustomerGroup>();
            foreach (var chatId in chatIds.Distinct()) result.Add(await GetCustomerGroupWithTokenAsync(token, chatId, cancellationToken));
            return result;
        }

        public async Task<WeComCustomerGroup> GetCustomerGroupAsync(string chatId, CancellationToken cancellationToken = default)
        {
            EnsureConfigured();
            if (string.IsNullOrWhiteSpace(chatId)) throw new ArgumentException("chat_id 不能为空。", nameof(chatId));
            var token = await GetAccessTokenAsync(cancellationToken);
            return await GetCustomerGroupWithTokenAsync(token, chatId, cancellationToken);
        }

        private async Task<WeComCustomerGroup> GetCustomerGroupWithTokenAsync(string token, string chatId, CancellationToken cancellationToken)
        {
            var json = await PostAsync($"/cgi-bin/externalcontact/groupchat/get?access_token={Uri.EscapeDataString(token)}", new JObject { ["chat_id"] = chatId, ["need_name"] = 1 }, cancellationToken);
            EnsureSuccess(json);
            var group = json["group_chat"];
            return new WeComCustomerGroup
            {
                ChatId = group?.Value<string>("chat_id") ?? chatId,
                Name = group?.Value<string>("name"),
                OwnerUserId = group?.Value<string>("owner"),
                Status = group?.Value<int?>("status") ?? 0,
                MemberCount = group?["member_list"]?.Count() ?? 0
            };
        }

        private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(_accessToken) && _accessTokenExpiresAtUtc > DateTime.UtcNow) return _accessToken;
            await _tokenLock.WaitAsync(cancellationToken);
            try
            {
                if (!string.IsNullOrWhiteSpace(_accessToken) && _accessTokenExpiresAtUtc > DateTime.UtcNow) return _accessToken;
                var json = await GetAsync($"/cgi-bin/gettoken?corpid={Uri.EscapeDataString(_options.CorpId)}&corpsecret={Uri.EscapeDataString(_options.CustomerContactSecret)}", cancellationToken);
                EnsureSuccess(json);
                var token = json.Value<string>("access_token");
                var expiresIn = json.Value<int?>("expires_in") ?? 7200;
                _accessToken = token;
                _accessTokenExpiresAtUtc = DateTime.UtcNow.AddSeconds(Math.Max(60, expiresIn - 300));
                return token;
            }
            finally { _tokenLock.Release(); }
        }

        private async Task<JObject> GetAsync(string path, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient(nameof(WeComApiClient));
            using var response = await client.GetAsync(path, cancellationToken);
            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return JObject.Parse(content);
        }

        private async Task<JObject> PostAsync(string path, JObject payload, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient(nameof(WeComApiClient));
            using var body = new StringContent(payload.ToString(Formatting.None), Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(path, body, cancellationToken);
            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return JObject.Parse(content);
        }

        private static JArray BuildAttachments(IEnumerable<WeComMessageAttachment> attachments)
        {
            var result = new JArray();
            foreach (var item in attachments)
            {
                var type = item.MessageType?.Trim().ToLowerInvariant();
                JObject content;
                switch (type)
                {
                    case "image": content = new JObject { ["media_id"] = item.MediaId, ["pic_url"] = item.PictureUrl }; break;
                    case "link": content = new JObject { ["title"] = item.Title, ["picurl"] = item.PictureUrl, ["desc"] = item.Description, ["url"] = item.Url }; break;
                    case "miniprogram": content = new JObject { ["title"] = item.Title, ["pic_media_id"] = item.MediaId, ["appid"] = item.AppId, ["page"] = item.Page }; break;
                    case "video":
                    case "file": content = new JObject { ["media_id"] = item.MediaId }; break;
                    default: throw new ArgumentException($"不支持的企业微信附件类型：{item.MessageType}");
                }
                foreach (var property in content.Properties().Where(x => x.Value.Type == JTokenType.Null).ToList()) property.Remove();
                result.Add(new JObject { ["msgtype"] = type, [type] = content });
            }
            return result;
        }

        private void EnsureConfigured()
        {
            if (!_options.Enabled) throw new InvalidOperationException("企业微信功能尚未启用，请配置 WeCom:Enabled=true。");
            if (string.IsNullOrWhiteSpace(_options.CorpId) || string.IsNullOrWhiteSpace(_options.CustomerContactSecret))
                throw new InvalidOperationException("企业微信 CorpId 或客户联系 Secret 尚未配置。");
        }

        private static void Validate(WeComMassMessageRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.ChatType != "single" && request.ChatType != "group") throw new ArgumentException("ChatType 只能是 single 或 group。");
            if (string.IsNullOrWhiteSpace(request.SenderUserId)) throw new ArgumentException("必须指定发送员工的企业微信 userid。");
            if (request.ChatType == "single" && (request.ExternalUserIds == null || request.ExternalUserIds.Count == 0)) throw new ArgumentException("至少指定一个企业微信客户 external_userid。");
            if (request.ChatType == "group" && (request.ChatIds == null || request.ChatIds.Count == 0)) throw new ArgumentException("至少指定一个企业微信客户群 chat_id。");
            if (string.IsNullOrWhiteSpace(request.Text) && (request.Attachments == null || request.Attachments.Count == 0)) throw new ArgumentException("消息文字和附件不能同时为空。");
            if (request.Attachments != null && request.Attachments.Count > 9) throw new ArgumentException("企业群发最多支持 9 个附件。");
        }

        private static void EnsureSuccess(JObject json)
        {
            var errorCode = json.Value<int?>("errcode") ?? -1;
            if (errorCode != 0) throw new WeComApiException(errorCode, json.Value<string>("errmsg") ?? "未知错误");
        }
    }
}
