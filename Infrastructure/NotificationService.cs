using System;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Infrastructure
{
    public class NotificationService : INotificationService
    {
        private const string AccountSid = "AC05fa19002defe7e7fe887309a126ed8a";
        private const string AuthToken = "24ba72cd92b9835abf8e3df679da1dd7";
        private const string EplusPhoneNumber = "+16473709095";

        private readonly ILogger _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public async Task SendMessageAsync(string phoneNumber, string message, string senderName)
        {
            try
            {
                TwilioClient.Init(AccountSid, AuthToken);

                await MessageResource.CreateAsync(
                    body: $"【舒誉】您好，这里是舒誉物流。{message}。操作员:{senderName}。客服电话：1-647-891-7666,微信yz890713，我们的网站：www.eplus-ex.com。",
                    from: new Twilio.Types.PhoneNumber(EplusPhoneNumber),
                    to: new Twilio.Types.PhoneNumber($"+1{phoneNumber}")
                );

            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Failed to send message to ${phoneNumber}. Error message: ${e.Message}");
            }
        }
    }
}
