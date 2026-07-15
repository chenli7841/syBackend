using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISystemService
    {
        Task<SystemPhotoEntity> AddPhotoSync(int id, string rawData);
        Task<IEnumerable<SystemPhotoEntity>> ListPhotosAsync(int? companyIds);
        Task<IEnumerable<SystemPhotoEntity>> ListMobilePhotosAsync(int? companyIds);
        Task<SystemSettingsEntity> GetSettingsAsync();
        Task UpdateSettingsAsync(SystemSettingsEntity settings);
        Task<SystemPhotoEntity> GetPhotoUploadUrl(int id, int companyId);
        Task<string> GetSystemImageUploadUrl(string propertyName, int companyId);
        Task<SystemPhotoEntity> GetMobilePhotoUploadUrl(int id, int companyId);
        Task UpdateCompanyKeyValue(List<Tuple<string, string>> keyValuePairs);
        Task<List<Tuple<string, string>>> GetCompanyKeyValue(List<string> keys, int companyId);
        Task SaveSystemPropertyImages(string name, string rawData);
        Task<IEnumerable<CompanyEntity>> GetSelectableCompaniesAsync();
        Task<int[]> ResolveCompanyIdsAsync(string requestedCompanyIds);
        Task<int?> GetLockedCompanyIdAsync();
        Task<string> GetLogoUrlAsync();
        Task<string> UploadLogoAsync(string rawData);
    }
}