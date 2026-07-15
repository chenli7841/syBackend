using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain;
using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Services
{
    public class SystemService : ISystemService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public SystemService(EplusDbContext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<SystemPhotoEntity> AddPhotoSync(int id, string rawData)
        {
            SystemPhoto photo;
            if (id == 0)
            {
                photo = new SystemPhoto() {Url = ""};
                await _context.SystemPhotos.AddAsync(photo);
                await _context.SaveChangesAsync();
            }
            else
            {
                photo = await _context.SystemPhotos.FirstAsync(p => p.Id == id);
            }
            var urlList = await _context.SystemPhotos.Select(p => p.Url).ToListAsync();
            var nextId = urlList.Select(u => Int32.Parse(u.Split("system/photos/pc")[1].Split(".png")[0])).Max() + 1;
            var photoUrl = await _storageService.UploadToAzureAsync(rawData, "system/photos/pc", $"{nextId}.png");
            photo.Url = photoUrl;
            await _context.SaveChangesAsync();

            return _mapper.Map<SystemPhotoEntity>(photo);
        }

        public async Task SaveSystemPropertyImages(string name, string rawData)
        {
            var imageURL = await _storageService.UploadToAzureAsync(rawData, "system/photos", $"{name}_{DateTime.Now.ToString("s")}");
            var property = await _context.CompanyKeyValues.FirstOrDefaultAsync(kv => kv.CompanyId == Config.COMPANY_ID && kv.Name == name);
            if (property == null)
            {
                await _context.CompanyKeyValues.AddAsync(new CompanyKeyValue
                {
                    CompanyId = Config.COMPANY_ID,
                    Name = name,
                    Content = imageURL
                });
            }
            else
            {
                property.Content = imageURL;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<SystemPhotoEntity> GetPhotoUploadUrl(int id, int companyId)
        {
            SystemPhoto photo;
            if (id == 0)
            {
                photo = new SystemPhoto() { Url = "", CompanyId = companyId };
                await _context.SystemPhotos.AddAsync(photo);
                await _context.SaveChangesAsync();
            }
            else
            {
                photo = await _context.SystemPhotos.FirstAsync(p => p.Id == id);
            }
            var urlList = await _context.SystemPhotos.Select(p => p.Url).ToListAsync();
            var validUrlList = urlList.Where(u =>
            {
                if (!u.Contains("system/photos/pc/")) return false;
                return Int32.TryParse(u.Split("system/photos/pc/")[1].Split(".png")[0], out _);
            });
            int nextId = 1;
            if (validUrlList.Any())
            {
                nextId = validUrlList.Select(u => Int32.Parse(u.Split("system/photos/pc/")[1].Split(".png")[0])).Max() + 1;
            }
            var photoUrl = _storageService.GetAzureUploadUrl("system/photos/pc", $"{nextId}.png");
            var fileUrl = _storageService.GetFileUrl("system/photos/pc", $"{nextId}.png");
            photo.Url = fileUrl;
            await _context.SaveChangesAsync();

            var photoEntity = _mapper.Map<SystemPhotoEntity>(photo);
            photoEntity.Url = photoUrl;
            return photoEntity;
        }

        public async Task<string> GetSystemImageUploadUrl(string propertyName, int companyId)
        {
            var photoUrl = _storageService.GetAzureUploadUrl($"system/{companyId}/images", $"{propertyName}.png");
            var fileUrl = _storageService.GetFileUrl($"system/{companyId}/images", $"{propertyName}.png");

            var property = await _context.CompanyKeyValues.FirstOrDefaultAsync(kv => kv.CompanyId == companyId && kv.Name == propertyName);
            if (property == null)
            {
                await _context.CompanyKeyValues.AddAsync(new CompanyKeyValue
                {
                    CompanyId = companyId,
                    Name = propertyName,
                    Content = fileUrl
                });
            }
            else
            {
                property.Content = fileUrl;
            }
            await _context.SaveChangesAsync();
            return photoUrl;
        }

        public async Task<SystemPhotoEntity> GetMobilePhotoUploadUrl(int id, int companyId)
        {
            BaseAdvert photo;
            if (id == 0)
            {
                photo = new BaseAdvert() { AdPictureKey = "", CompanyId = companyId, AdType = 26, IsShow = true, IsDel = false };
                await _context.BaseAdverts.AddAsync(photo);
                await _context.SaveChangesAsync();
            }
            else
            {
                photo = await _context.BaseAdverts.FirstAsync(p => p.Id == id);
            }
            var urlList = await _context.BaseAdverts.Where(b => b.IsShow == true && b.IsDel == false && b.AdType == 26).Select(p => p.AdPictureKey).ToListAsync();
            var nextId = urlList.Select(u =>
            {
                var parts = u.Split("system/photos/mobile/");
                if (parts.Length < 2) return 0;
                parts = parts[1].Split(".png");
                if (parts.Length < 2) return 0;
                return Int32.Parse(parts[0]);
            }).Max() + 1;
            var uploadUrl = _storageService.GetAzureUploadUrl("system/photos/mobile", $"{nextId}.png");
            var fileUrl = _storageService.GetFileUrl("system/photos/mobile", $"{nextId}.png");
            photo.AdPictureKey = fileUrl;
            await _context.SaveChangesAsync();

            var photoEntity = _mapper.Map<SystemPhotoEntity>(photo);
            photoEntity.Url = uploadUrl;
            return photoEntity;
        }

        public async Task<IEnumerable<SystemPhotoEntity>> ListPhotosAsync(int? companyIds)
        {
            return await _context.SystemPhotos.Where(p => p.CompanyId == (companyIds ?? Config.COMPANY_ID)).Select(p => _mapper.Map<SystemPhotoEntity>(p)).ToListAsync();
        }

        public async Task<IEnumerable<SystemPhotoEntity>> ListMobilePhotosAsync(int? companyIds)
        {
            return await _context.BaseAdverts.Where(a => a.AdType == 26 && a.IsDel == false && a.IsShow == true && a.CompanyId == (companyIds ?? Config.COMPANY_ID)).Select(p => _mapper.Map<SystemPhotoEntity>(p)).ToListAsync();
        }

        public async Task<SystemSettingsEntity> GetSettingsAsync()
        {
            var dbResult = (await _context.SystemSettings.FirstOrDefaultAsync()) ?? new SystemSetting();
            var result = _mapper.Map<SystemSettingsEntity>(dbResult);

            var weChatPayMethod = await _context.PayMethods.FirstAsync(p => p.Name == "微信");
            result.WeChatServiceChat= weChatPayMethod.ServiceCharge;

            var dbSettings = await _context.BaseSetings.ToListAsync();
            result.DeliveryInitialCost = GetFromBaseSetting(dbSettings, "record_freight_initial_cost");
            result.DeliveryRenewalCost = GetFromBaseSetting(dbSettings, "record_freight_renewal_cost");
            result.DeliveryLargeHandlingCost = GetFromBaseSetting(dbSettings, "record_freight_large_handling_cost");
            result.DeliveryOversizedHandlingCost = GetFromBaseSetting(dbSettings, "record_freight_oversized_handling_cost");
            result.DeliveryDistanceAdditionalCost = GetFromBaseSetting(dbSettings, "record_freight_distance_additional_cost");
            result.CostStorageTimeout = GetFromBaseSetting(dbSettings, "record_order_cost_storage_timeout");
            result.LockedCompanyId = await GetLockedCompanyIdAsync();
            result.LogoUrl = await GetLogoUrlAsync();

            return result;
        }

        public async Task UpdateCompanyKeyValue(List<Tuple<string, string>> keyValuePairs)
        {
            foreach(var keyValue in keyValuePairs)
            {
                var record = await _context.CompanyKeyValues.FirstOrDefaultAsync(kv => kv.CompanyId == Config.COMPANY_ID && kv.Name == keyValue.Item1);
                if (record != null)
                {
                    record.Content = keyValue.Item2;
                }
                else
                {
                    await _context.CompanyKeyValues.AddAsync(new CompanyKeyValue
                    {
                        CompanyId = Config.COMPANY_ID,
                        Name = keyValue.Item1,
                        Content = keyValue.Item2,
                    });
                }
            }
            await _context.SaveChangesAsync();

        }

        public async Task<List<Tuple<string, string>>> GetCompanyKeyValue(List<string> keys, int companyId)
        {
            var keyValues = await _context.CompanyKeyValues.Where(kv => kv.CompanyId == companyId && keys.Contains(kv.Name)).ToListAsync();
            return keyValues.Select(kv => new Tuple<string, string>(kv.Name, kv.Content)).ToList();
        }
    

        public async Task UpdateSettingsAsync(SystemSettingsEntity model)
        {
            var settings = await _context.SystemSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new SystemSetting() { SchedulePickUpText = model.SchedulePickUpText };
                await _context.SystemSettings.AddAsync(settings);
            }
            else
            {
                settings.SchedulePickUpText = model.SchedulePickUpText;
            }

            var weChatPayMethod = await _context.PayMethods.FirstAsync(p => p.Name == "微信");
            weChatPayMethod.ServiceCharge = model.WeChatServiceChat;

            await PersistToBaseSetting(model.DeliveryInitialCost, "record_freight_initial_cost");
            await PersistToBaseSetting(model.DeliveryRenewalCost, "record_freight_renewal_cost");
            await PersistToBaseSetting(model.DeliveryLargeHandlingCost, "record_freight_large_handling_cost");
            await PersistToBaseSetting(model.DeliveryOversizedHandlingCost, "record_freight_oversized_handling_cost");
            await PersistToBaseSetting(model.DeliveryDistanceAdditionalCost, "record_freight_distance_additional_cost");
            await PersistToBaseSetting(model.CostStorageTimeout, "record_order_cost_storage_timeout");
            await SetLockedCompanyIdAsync(model.LockedCompanyId);

            await _context.SaveChangesAsync();
        }

        private decimal GetFromBaseSetting(IEnumerable<BaseSeting> dbSettings, string costName)
        {
            var dbValue = dbSettings.FirstOrDefault(s => s.SetKey == costName);
            return dbValue == null ? 0 : decimal.Parse(dbValue.SetValue);
        }

        private async Task PersistToBaseSetting(decimal value, string costName)
        {
            var dbRecord = await _context.BaseSetings.FirstAsync(b => b.SetKey == costName);
            dbRecord.SetValue = value.ToString(CultureInfo.InvariantCulture);
        }

        private const string LockedCompanyIdKey = "record_locked_company_id";

        public async Task<int?> GetLockedCompanyIdAsync()
        {
            var dbRecord = await _context.BaseSetings.FirstOrDefaultAsync(b => b.SetKey == LockedCompanyIdKey);
            if (dbRecord == null || string.IsNullOrWhiteSpace(dbRecord.SetValue))
            {
                return null;
            }
            if (!int.TryParse(dbRecord.SetValue, out var parsed) || parsed == 0)
            {
                return null;
            }
            return parsed;
        }

        private async Task SetLockedCompanyIdAsync(int? companyId)
        {
            var value = companyId.HasValue ? companyId.Value.ToString(CultureInfo.InvariantCulture) : "0";
            var dbRecord = await _context.BaseSetings.FirstOrDefaultAsync(b => b.SetKey == LockedCompanyIdKey);
            if (dbRecord == null)
            {
                await _context.BaseSetings.AddAsync(new BaseSeting
                {
                    SetKey = LockedCompanyIdKey,
                    SetValue = value,
                    ValueType = false,
                    Type = "int",
                    Remark = "锁定公司:非0时,后台所有公司选择下拉菜单强制只显示/选择该公司",
                    CreateTime = DateTime.UtcNow,
                });
            }
            else
            {
                dbRecord.SetValue = value;
                dbRecord.UpdateTime = DateTime.UtcNow;
            }
        }

        private const string LogoUrlKey = "record_site_logo_url";

        public async Task<string> GetLogoUrlAsync()
        {
            var dbRecord = await _context.BaseSetings.FirstOrDefaultAsync(b => b.SetKey == LogoUrlKey);
            return string.IsNullOrWhiteSpace(dbRecord?.SetValue) ? null : dbRecord.SetValue;
        }

        public async Task<string> UploadLogoAsync(string rawData)
        {
            var logoUrl = await _storageService.UploadToAzureAsync(rawData, "system/logo", $"logo_{DateTime.Now:yyyyMMddHHmmssfff}.png");

            var dbRecord = await _context.BaseSetings.FirstOrDefaultAsync(b => b.SetKey == LogoUrlKey);
            if (dbRecord == null)
            {
                await _context.BaseSetings.AddAsync(new BaseSeting
                {
                    SetKey = LogoUrlKey,
                    SetValue = logoUrl,
                    ValueType = false,
                    Type = "string",
                    Remark = "网站左上角 Logo 图片地址",
                    CreateTime = DateTime.UtcNow,
                });
            }
            else
            {
                dbRecord.SetValue = logoUrl;
                dbRecord.UpdateTime = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();

            return logoUrl;
        }

        public async Task<IEnumerable<CompanyEntity>> GetSelectableCompaniesAsync()
        {
            var all = await _context.Companies.ToListAsync();
            var lockedCompanyId = await GetLockedCompanyIdAsync();
            if (lockedCompanyId.HasValue)
            {
                var locked = all.Where(c => c.Id == lockedCompanyId.Value).ToList();
                if (locked.Count > 0)
                {
                    return locked.Select(c => _mapper.Map<CompanyEntity>(c));
                }
            }
            return all.Select(c => _mapper.Map<CompanyEntity>(c));
        }

        public async Task<int[]> ResolveCompanyIdsAsync(string requestedCompanyIds)
        {
            var lockedCompanyId = await GetLockedCompanyIdAsync();
            if (lockedCompanyId.HasValue)
            {
                return new[] { lockedCompanyId.Value };
            }
            return (requestedCompanyIds ?? "").Split(",").Where(id => int.TryParse(id, out int parsed)).Select(id => int.Parse(id)).ToArray();
        }
    }
}
