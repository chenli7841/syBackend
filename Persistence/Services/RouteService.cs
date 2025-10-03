using Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence.Data;
using Domain;

namespace Persistence.Services
{
    public class RouteService : IRouteService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public RouteService(EplusDbContext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<IEnumerable<RouteEntity>> ListAsync()
        {
            var routes = await _context.Routes.Include(r => r.Warehouse).Where(r => r.IsFromChina && r.CompanyId == Config.COMPANY_ID)
                .Select(r => _mapper.Map<RouteEntity>(r))
                .ToListAsync();
            return routes;
        }

        public async Task<RouteEntity> GetAsync(int id)
        {
            var route = await _context.Routes.FirstAsync(r => r.Id == id && r.CompanyId == Config.COMPANY_ID);
            var result = _mapper.Map<RouteEntity>(route);
            
            if (string.IsNullOrEmpty(route.Price))
            {
                return result;
            }

            result.ItemPrices = JsonConvert.DeserializeObject<List<RouteItemPrice>>(route.Price);
            return result;
        }

        public async Task<RouteEntity> SaveAsync(RouteEntity model, string photoData)
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
                var photoUrl = await _storageService.UploadAsync(photoData, $"route/{model.Id}.png");
                var route = await _context.Routes.FirstAsync(r => r.Id == model.Id);
                route.Photo = photoUrl;
                model.Photo = photoUrl;
                await _context.SaveChangesAsync();
            }

            return model;
        }

        private async Task<Route> UpdateAsync(RouteEntity model)
        {
            var route = await _context.Routes.FirstAsync(r => r.Id == model.Id);
            route.Name = model.Name;
            route.Code = model.Code;
            route.IsFromChina = model.Type != RouteType.China;
            route.Type = (int)model.Type;
            route.WarehouseId = model.WarehouseId;
            route.FixedPrice = model.FixedPrice;
            route.Description = model.Description;
            route.SupportWechat = model.SupportWechat;
            route.SupportDescription = model.SupportDescription;
            route.Price = JsonConvert.SerializeObject(model.ItemPrices);
            route.DisplaySequence = model.DisplaySequence;
            route.CompanyId = Config.COMPANY_ID;
            await _context.SaveChangesAsync();
            return route;
        }

        private async Task<Route> CreateAsync(RouteEntity model)
        {
            var route = new Route()
            {
                Name = model.Name,
                Code = model.Code,
                WarehouseId = model.WarehouseId,
                IsFromChina = model.Type != RouteType.China,
                Type = (int)model.Type,
                Price = JsonConvert.SerializeObject(model.ItemPrices),
                FixedPrice = model.FixedPrice,
                SupportWechat = model.SupportWechat,
                SupportDescription = model.SupportDescription,
                DisplaySequence = model.DisplaySequence,
                CompanyId = Config.COMPANY_ID,
            };
            await _context.Routes.AddAsync(route);
            await _context.SaveChangesAsync();
            
            return route;
        }

        public async Task HideAsync(int id)
        {
            var route = _context.Routes.First(r => r.Id == id);
            route.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task ShowAsync(int id)
        {
            var route = _context.Routes.First(r => r.Id == id);
            route.IsDeleted = false;
            await _context.SaveChangesAsync();
        }

        public async Task ToggleIsRegular(int id)
        {
            var route = _context.Routes.First(r => r.Id == id);
            route.IsRegular = !route.IsRegular;
            await _context.SaveChangesAsync();
        }

        public async Task<RoutePermissions> ListPermissionsAsync(int id)
        {
            var users = await _context.Users.OrderBy(u => u.OrderStartNumber).Select(u => new RouteUserPermission()
            {
                UserId = u.Id,
                UserCode = u.OrderStartNumber,
                IsVisible = true,
            }).ToListAsync();

            var bannedUsers = await _context.BannedUserRoutes.Where(r => r.RouteId == id).ToListAsync();

            foreach (var bannedUserRoute in bannedUsers)
            {
                users.First(u => u.UserId == bannedUserRoute.UserId).IsVisible = false;
            }

            var route = await _context.Routes.FirstAsync(r => r.Id == id);

            var result = new RoutePermissions()
            {
                Id = route.Id,
                Name = route.Name,
                UserPermissions = users
            };

            return result;
        }

        public async Task RemovePermissionsAsync(int id)
        {
            var users = await _context.Users.Where(u => u.Role != (int)RoleType.Admin).Select(u => u.Id).ToListAsync();
            var bannedUsers = await _context.BannedUserRoutes.Where(b => b.RouteId == id).Select(b => b.UserId).ToListAsync();
            var usersToBan = new List<BannedUserRoute>();
            
            foreach (var user in users)
            {
                if (bannedUsers.Contains(user))
                {
                    continue;
                }

                usersToBan.Add(new BannedUserRoute() { UserId = user, RouteId = id });
            }

            await _context.BannedUserRoutes.AddRangeAsync(usersToBan);
            await _context.SaveChangesAsync();
        }

        public async Task AddAllPermissionsAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync($"DELETE FROM banned_user_route WHERE RouteId={id}");
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync($@"
                INSERT INTO deleted_route SELECT * FROM route WHERE Id = {id};
                DELETE FROM banned_user_route WHERE RouteId = {id}
");
            var route = _context.Routes.First(r => r.Id == id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
        }
    }
}
