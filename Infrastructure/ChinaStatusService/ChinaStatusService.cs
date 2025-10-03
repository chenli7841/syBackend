using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.ChinaStatusService
{
    public class ChinaStatusService : IChinaStatusService
    {
        private const string QueryUrl = "http://poll.kuaidi100.com/poll/query.do";
        private const string CarrierDetectionUrl = "http://www.kuaidi100.com/autonumber/auto?num={0}&key={1}";
        public const string SubscribeUrl = "https://poll.kuaidi100.com/poll";

        private const string ApiKey = "SjQPhiti4983";
        private const string ApiCustomer = "1D5AC504807A4578DEC3DE5570C5710A";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public ChinaStatusService(IHttpClientFactory httpClientFactory, ILogger<ChinaStatusService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ChinaOrder> GetStatusAsync(string carrier, string number, string phone)
        {
            var param = "{\"com\":\"" + carrier + "\",\"num\":\"" + number + "\",\"phone\":\"" + phone + "\"}";
            var sign = GetSign(param + ApiKey + ApiCustomer);
            var payload = new Dictionary<string, string>
            {
                {"param", param},
                {"customer", ApiCustomer},
                {"sign", sign}
            };

            try
            {
                var client = _httpClientFactory.CreateClient();
                using var multipartFormDataContent = new FormUrlEncodedContent(payload);
                var postResponse = await client.PostAsync(QueryUrl, multipartFormDataContent);
                var postContent = postResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ChinaOrder>(postContent);
                return result;
            }
            catch (Exception e)
            {
                var message = $"Unable to resolve status for domestic order: {number} with carrier: {carrier} because: {e.Message}";
                _logger.LogError(message, e);
            }

            return ChinaOrder.NullChinaOrder;
        }

        public async Task<bool> SubscribeStatus(string carrier, string number)
        {
            var callbackUrl = @"https://eplusapi.azurewebsites.net/api/Orders/Kuaidi";
            var parameters = "{\"com\":\"1\",\"callbackurl\":\"" + callbackUrl + "\"}";
            var payload = new Dictionary<string, string>
            {
                {"schema", "json"},
                {"company", carrier},
                {"number", number},
                {"key", ApiKey},
                {"parameters", parameters}
            };

            try
            {
                var client = _httpClientFactory.CreateClient();
                using var multipartFormDataContent = new FormUrlEncodedContent(payload);
                var postResponse = await client.PostAsync(SubscribeUrl, multipartFormDataContent);
                // todo: get result from response content
                return postResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                var message = $"Unable to subscribe status for domestic order: {number} with carrier: {carrier} because: {e.Message}";
                _logger.LogError(message, e);
                return false;
            }
        }

        public ChinaCarrier DetectCarrier(string number)
        {
            //using (var client = new WebClient())
            //{
            //    client.Encoding = Encoding.UTF8;
            //    var url = string.Format(CarrierDetectionUrl, number, ApiKey);

            //    try
            //    {
            //        var backUpResult = client.DownloadString(url);
            //        var result = JsonConvert.DeserializeObject<PossibleChinaCarrier[]>(backUpResult);
            //        if (result == null || result.Length == 0)
            //        {
            //            return null;
            //        }

            //        return new ChinaCarrier()
            //        {
            //            Code = result[0].Code
            //        };
            //    }
            //    catch (Exception ex)
            //    {
            //        const string message = "Unable to resolve carrier for domestic order number: {0} with reason: {1}";
            //        //CommonManager.LogMessage(string.Format(message, number, ex.Message));
            //        return null;
            //    }
            //}
            return null;
        }

        private static string GetSign(string data)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
