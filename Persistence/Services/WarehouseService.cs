using System;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Domain;

namespace Persistence.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private readonly IDateTime _dateTime;

        public WarehouseService(EplusDbContext context, IMapper mapper, IStorageService storageService, IDateTime dateTime)
        {
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
            _dateTime = dateTime;
        }

        public async Task<IEnumerable<WarehouseEntity>> ListAsync()
        {
            var result = await _context.Warehouses
                .Where(r => r.CompanyId == Config.COMPANY_ID)
                .Select(r => _mapper.Map<WarehouseEntity>(r))
                .ToListAsync();
            return result;
        }

        public async Task<WarehouseEntity> GetAsync(int id)
        {
            var route = await _context.Warehouses.FirstAsync(r => r.Id == id && r.CompanyId == Config.COMPANY_ID);
            return _mapper.Map<WarehouseEntity>(route);
        }

        public async Task<WarehouseEntity> SaveAsync(WarehouseEntity model, string photoData)
        {
            if (model.Id == 0)
            {
                var result = await CreateAsync(model);
                model.Id = result.Id;
            }
            else
            {
                var result = await UpdateAsync(model);
            }

            if (!string.IsNullOrEmpty(photoData))
            {
                var photoUrl = await _storageService.UploadAsync(photoData, $"warehouse/{model.Id}.png");
                var warehouse = await _context.Warehouses.FirstAsync(r => r.Id == model.Id);
                warehouse.Photo = photoUrl;
                model.Photo = photoUrl;
                await _context.SaveChangesAsync();
            }

            return model;
        }

        private async Task<Warehouse> UpdateAsync(WarehouseEntity model)
        {
            var warehouse = await _context.Warehouses.FirstAsync(r => r.Id == model.Id);
            warehouse.Name = model.Name;
            warehouse.Location = model.Location;
            warehouse.Contact = model.Contact;
            warehouse.DisplaySequence = model.DisplaySequence;
            await _context.SaveChangesAsync();
            return warehouse;
        }

        private async Task<Warehouse> CreateAsync(WarehouseEntity model)
        {
            var warehouse = new Warehouse()
            {
                Name = model.Name,
                Location = model.Location,
                Contact = model.Contact,
                DisplaySequence = model.DisplaySequence,
                CompanyId = Config.COMPANY_ID,
            };
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            
            return warehouse;
        }

        public async Task DeleteAsync(int id)
        {
            var warehouse = _context.Warehouses.First(r => r.Id == id && r.CompanyId == Config.COMPANY_ID);
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}
