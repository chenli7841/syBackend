using Domain.Entities;

namespace Domain.Services
{
    public interface ICacheService
    {
        RingCentralCacheEntity getRingCentralClient(int userId);
        void setRingCentralClient(int userId, RingCentralCacheEntity value);
        RingCentralCredentialEntity[] getRingCentralCredentials();
        void setRingCentralCredentials(RingCentralCredentialEntity[] value);
    }
}
