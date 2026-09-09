using System.Threading;
using System.Threading.Tasks;

namespace Common.WeCom
{
    public interface IWeComCustomerEventService
    {
        Task<bool> ProcessDecryptedEventAsync(string xml, int companyId, CancellationToken cancellationToken = default);
    }
}
