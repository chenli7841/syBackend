namespace Infrastructure.WeCom
{
    public class WeComOptions
    {
        public const string SectionName = "WeCom";
        public bool Enabled { get; set; }
        public int CompanyId { get; set; }
        public string CorpId { get; set; }
        public string CustomerContactSecret { get; set; }
        public string ApiBaseUrl { get; set; } = "https://qyapi.weixin.qq.com";
        public string CallbackToken { get; set; }
        public string CallbackEncodingAesKey { get; set; }
    }
}
