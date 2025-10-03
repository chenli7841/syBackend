using RingCentral;

namespace Domain.Entities
{
    public class RingCentralCacheEntity
    {
        public RestClient Client { get; set; }
        public string FromNumber { get; set; }
    }

    public class RingCentralCredentialEntity
    {
        public long UserId { get; set; }
        public string ApplicationName { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string UserName { get; set; }
        public string Extension { get; set; }
        public string Password { get; set; }
        public string FromNumber { get; set; }
    }
}