using Domain.Services;
using Microsoft.Extensions.Caching.Memory;
using Domain.Entities;

namespace Persistence.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private static string RINGCENTRAL_CREDENTIAL_KEY = "ringcentral_credential";
        private static string RINGCENTRAL_CLIENT_KEY = "ringcentral_client_key_";
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public RingCentralCacheEntity getRingCentralClient(int userId)
        {
            if (_cache.TryGetValue($"{RINGCENTRAL_CLIENT_KEY}{userId}", out RingCentralCacheEntity value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public void setRingCentralClient(int userId, RingCentralCacheEntity value)
        {
            _cache.Set($"{RINGCENTRAL_CLIENT_KEY}{userId}", value);
        }

        public RingCentralCredentialEntity[] getRingCentralCredentials()
        {
            if (_cache.TryGetValue(RINGCENTRAL_CREDENTIAL_KEY, out RingCentralCredentialEntity[] value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public void setRingCentralCredentials(RingCentralCredentialEntity[] value)
        {
            _cache.Set(RINGCENTRAL_CREDENTIAL_KEY, value);
        }
    }
}
