using System;

namespace Infrastructure.WeCom
{
    public class WeComApiException : Exception
    {
        public WeComApiException(int errorCode, string message)
            : base($"企业微信接口调用失败（{errorCode}）：{message}")
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }
}
