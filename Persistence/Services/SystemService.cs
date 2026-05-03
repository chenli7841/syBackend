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

        public async Task<SystemPhotoEntity> GetPhotoUploadUrl(int id)
        {
            SystemPhoto photo;
            if (id == 0)
            {
                photo = new SystemPhoto() { Url = "" };
                await _context.SystemPhotos.AddAsync(photo);
                await _context.SaveChangesAsync();
            }
            else
            {
                photo = await _context.SystemPhotos.FirstAsync(p => p.Id == id);
            }
            var urlList = await _context.SystemPhotos.Select(p => p.Url).ToListAsync();
            var nextId = urlList.Select(u => Int32.Parse(u.Split("system/photos/pc/")[1].Split(".png")[0])).Max() + 1;
            var photoUrl = _storageService.GetAzureUploadUrl("system/photos/pc", $"{nextId}.png");
            var fileUrl = _storageService.GetFileUrl("system/photos/pc", $"{nextId}.png");
            photo.Url = fileUrl;
            await _context.SaveChangesAsync();

            var photoEntity = _mapper.Map<SystemPhotoEntity>(photo);
            photo.Url = photoUrl;
            return photoEntity;
        }

        public async Task<string> GetSystemImageUploadUrl(string propertyName)
        {
            var photoUrl = _storageService.GetAzureUploadUrl("system/images", $"{propertyName}.png");
            var fileUrl = _storageService.GetFileUrl("system/images", $"{propertyName}.png");

            var property = await _context.CompanyKeyValues.FirstOrDefaultAsync(kv => kv.CompanyId == Config.COMPANY_ID && kv.Name == propertyName);
            if (property == null)
            {
                await _context.CompanyKeyValues.AddAsync(new CompanyKeyValue
                {
                    CompanyId = Config.COMPANY_ID,
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

        public async Task<SystemPhotoEntity> GetMobilePhotoUploadUrl(int id)
        {
            BaseAdvert photo;
            if (id == 0)
            {
                photo = new BaseAdvert() { AdPictureKey = "" };
                await _context.BaseAdverts.AddAsync(photo);
                await _context.SaveChangesAsync();
            }
            else
            {
                photo = await _context.BaseAdverts.FirstAsync(p => p.Id == id);
            }
            var urlList = await _context.BaseAdverts.Where(b => b.IsShow == true && b.IsDel == false && b.CompanyId == Config.COMPANY_ID && b.AdType == 26).Select(p => p.AdPictureKey).ToListAsync();
            var nextId = urlList.Select(u => Int32.Parse(u.Split("system/photos/mobile/")[1].Split(".png")[0])).Max() + 1;
            var uploadUrl = _storageService.GetAzureUploadUrl("system/photos/mobile", $"{nextId}.png");
            var fileUrl = _storageService.GetFileUrl("system/photos/mobile", $"{nextId}.png");
            photo.AdPictureKey = fileUrl;
            await _context.SaveChangesAsync();

            var photoEntity = _mapper.Map<SystemPhotoEntity>(photo);
            photoEntity.Url = uploadUrl;
            return photoEntity;
        }

        public async Task<IEnumerable<SystemPhotoEntity>> ListPhotosAsync()
        {
            return await _context.SystemPhotos.Where(p => p.CompanyId == Config.COMPANY_ID).Select(p => _mapper.Map<SystemPhotoEntity>(p)).ToListAsync();
        }

        public async Task<IEnumerable<SystemPhotoEntity>> ListMobilePhotosAsync()
        {
            return await _context.BaseAdverts.Where(a => a.AdType == 26 && a.IsDel == false && a.IsShow == true && a.CompanyId == Config.COMPANY_ID).Select(p => _mapper.Map<SystemPhotoEntity>(p)).ToListAsync();
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

        public async Task<List<Tuple<string, string>>> GetCompanyKeyValue(List<string> keys)
        {
            var keyValues = await _context.CompanyKeyValues.Where(kv => kv.CompanyId == Config.COMPANY_ID && keys.Contains(kv.Name)).ToListAsync();
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
    }
}
