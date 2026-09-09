using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Common.WeCom;
using Infrastructure.WeCom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("wecom/callback")]
    public class WeComCallbackController : ControllerBase
    {
        private readonly IWeComCallbackCrypt _crypt;
        private readonly IWeComCustomerEventService _eventService;
        private readonly WeComOptions _options;

        public WeComCallbackController(IWeComCallbackCrypt crypt, IWeComCustomerEventService eventService, WeComOptions options)
        {
            _crypt = crypt;
            _eventService = eventService;
            _options = options;
        }

        [HttpGet]
        public IActionResult Verify(
            [FromQuery(Name = "msg_signature")] string signature,
            [FromQuery] string timestamp,
            [FromQuery] string nonce,
            [FromQuery] string echostr)
        {
            try
            {
                var echo = _crypt.VerifyUrl(signature, timestamp, nonce, echostr);
                return Content(echo, "text/plain");
            }
            catch (CryptographicException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [RequestSizeLimit(1024 * 1024)]
        public async Task<IActionResult> Receive(
            [FromQuery(Name = "msg_signature")] string signature,
            [FromQuery] string timestamp,
            [FromQuery] string nonce,
            CancellationToken cancellationToken)
        {
            string encryptedXml;
            using (var reader = new StreamReader(Request.Body))
                encryptedXml = await reader.ReadToEndAsync();

            try
            {
                var xml = _crypt.DecryptMessage(signature, timestamp, nonce, encryptedXml);
                await _eventService.ProcessDecryptedEventAsync(xml, _options.CompanyId, cancellationToken);
                return Content("success", "text/plain");
            }
            catch (CryptographicException)
            {
                return Unauthorized();
            }
        }
    }
}
