using System.Threading.Tasks;

namespace Common
{
    public interface IStorageService
    {
        Task<string> UploadToAzureAsync(string rawData, string folderPath, string fileName);
        Task<string> UploadAsync(string data, string filename);
        Task DeleteAsync(string filename);
        string GetAzureUploadUrl(string folderPath, string fileName);
        string GetFileUrl(string folderPath, string fileName);
    }
}