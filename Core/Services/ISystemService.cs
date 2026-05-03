using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<string> GetSystemImageUploadUrl(string propertyName);
        Task<SystemPhotoEntity> GetMobilePhotoUploadUrl(int id);
        Task UpdateCompanyKeyValue(List<Tuple<string, string>> keyValuePairs);
        Task<List<Tuple<string, string>>> GetCompanyKeyValue(List<string> keys);
        Task SaveSystemPropertyImages(string name, string rawData);
    }
}