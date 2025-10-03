using Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Services
{
    public class DeliverProgressService : IDeliverProgressService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly IBatchService _batchService;

        public DeliverProgressService(EplusDbContext context, IMapper mapper, IDateTime dateTime, IBatchService batchService)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;
            _batchService = batchService;
        }

        public async Task<IEnumerable<DeliverProgressEntity>> ListAsync()
        {
            var result = await _context.DeliverProgresses.Include(r => r.Route).OrderBy(r => r.Hide)
                .Select(r => _mapper.Map<DeliverProgressEntity>(r))
                .ToListAsync();
            return result;
        }

        public async Task<DeliverProgressEntity> GetAsync(int id)
        {
            var result = await _context.DeliverProgresses.FirstAsync(r => r.Id == id);
            return _mapper.Map<DeliverProgressEntity>(result);
        }

        public async Task<DeliverProgressEntity> SaveAsync(DeliverProgressEntity model)
        {
            if (model.Id == 0)
            {
                var result = await CreateAsync(model);
                model.Id = result.Id;

                var batch = new BatchEntity()
                {
                    Name = model.Name + "批次",
                    GroupType = BatchGroupType.LoadDelivery,
                    DateCreated = _dateTime.UserNow,
                    ProgressId = result.Id
                };
                await _batchService.SaveAsync(batch);
                return model;
            }
            else
            {
                var result = await UpdateAsync(model);
                return model;
            }
        }

        private async Task<DeliverProgress> UpdateAsync(DeliverProgressEntity model)
        {
            var deliverProgress = await _context.DeliverProgresses.FirstAsync(r => r.Id == model.Id);
            deliverProgress.Name = model.Name;
            deliverProgress.Percent = model.Percent;
            deliverProgress.Description = model.Description;
            deliverProgress.RouteId = model.RouteId;
            await _context.SaveChangesAsync();
            return deliverProgress;
        }

        private async Task<DeliverProgress> CreateAsync(DeliverProgressEntity model)
        {
            var deliverProgress = new DeliverProgress()
            {
                Name = model.Name,
                Percent = model.Percent,
                Description = model.Description,
                RouteId = model.RouteId
            };
            await _context.DeliverProgresses.AddAsync(deliverProgress);
            await _context.SaveChangesAsync();
            
            return deliverProgress;
        }

        public async Task DeleteAsync(int id)
        {
            var route = _context.DeliverProgresses.First(r => r.Id == id);
            route.Hide = true;
            await _context.SaveChangesAsync();
        }
    }
}
