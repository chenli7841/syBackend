using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class LocationService : ILocationService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;

        public LocationService(EplusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(PickUpLocationEntity location, int? belongsToId)
        {
            _context.PickUpLocations.Add(new PickUpLocation
            {
                Name = location.Name,
                DistrictAdditionalCost = location.DistrictAdditionalCost,
                IsDel = false,
                BelongsToId = belongsToId,
                Number = location.Number,
                Version = location.Version,
                DetailArea = location.DetailArea,
                LatAndLng = location.LatAndLng,
                AreaId = location.AreaId,
                Visible = true,
                Note = location.Note,
            });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PickUpLocationAreaEntity>> ListAreas()
        {
            return await _context.Areas
                .Select(a => _mapper.Map<PickUpLocationAreaEntity>(a))
                .ToListAsync();
        }
    }
}
