using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public interface ISystemService
    {
        Task<SystemPhotoEntity> AddPhotoSync(int id, string rawData);
        Task<IEnumerable<SystemPhotoEntity>> ListPhotosAsync();
        Task<IEnumerable<SystemPhotoEntity>> ListMobilePhotosAsync();
        Task<SystemSettingsEntity> GetSettingsAsync();
        Task UpdateSettingsAsync(SystemSettingsEntity settings);
        Task<SystemPhotoEntity> GetPhotoUploadUrl(int id);
        Task<SystemPhotoEntity> GetMobilePhotoUploadUrl(int id);
    }
}