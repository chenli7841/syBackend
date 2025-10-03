using System.Threading.Tasks;

namespace Common
{
    public interface IStorageService
    {
        Task<string> UploadAsync(string data, string filename);
        Task DeleteAsync(string filename);
    }
}