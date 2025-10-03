using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
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
            var nextId = urlList.Select(u => Int32.Parse(u.Split("system/")[1].Split(".png")[0])).Max() + 1;
            var photoUrl = await _storageService.UploadAsync(rawData, $"system/{nextId}.png");
            photo.Url = photoUrl;
            await _context.SaveChangesAsync();

            return _mapper.Map<SystemPhotoEntity>(photo);
        }

        public async Task<IEnumerable<SystemPhotoEntity>> ListPhotosAsync()
        {
            return await _context.SystemPhotos.Select(p => _mapper.Map<SystemPhotoEntity>(p)).ToListAsync();
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
