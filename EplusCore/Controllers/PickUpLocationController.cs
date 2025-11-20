using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Services;
using WebUI.Models;
using WebUI.Models.ViewModels;
using Domain;

namespace WebUI.Controllers
{
    public class PickUpLocationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStatService _statService;
        private readonly ILocationService _locationService;

        const int LARGE_PAGE_SIZE = 999;

        public PickUpLocationController(
            IUserService userService,
            IMapper mapper,
            IStatService statService,
            ILocationService locationService)
        {
            _userService = userService;
            _statService = statService;
            _locationService = locationService;
        }

        public async Task<IActionResult> Inventory(int numberOfMonths = 3, int version = 1)
        {
            try
            {
                var viewModel = new PickUpLocationInventoryViewModel();
                var locations = (await _userService.ListPickUpLocationsAsync(version)).OrderBy(r => r.Number);
                var areas = await _locationService.ListAreas();
                var users = await _userService.ListAsync(new Domain.Models.UserListFilterOptions
                {
                    RoleToSearch = Domain.Enums.RoleType.Advanced,
                    PageSize = LARGE_PAGE_SIZE
                });
                viewModel.Agents = users.Items;
                var stats = await _statService.GetPickUpLocationStatistics(numberOfMonths);
                foreach (var l in locations)
                {
                    var stat = stats.FirstOrDefault(s => s.LocationId == l.Id);
                    if (stat != null)
                    {
                        l.NumberOfUsers = stat.NumberOfUsers;
                        l.RecentItemTotalWeightKg = stat.RecentItemTotalWeightKg;
                    }
                }
                viewModel.Locations = locations;
                viewModel.Areas = areas;
                return View("Inventory", viewModel);
            }
            catch(Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }

        public async Task<IActionResult> TogglePickUpLocationVisibility(int id)
        {
            await _userService.TogglePickUpLocationVisibilityAsync(id);
            return await Inventory();
        }

        public async Task<IActionResult> TransferUsers(int fromPickupLocationId, int toPickupLocationId)
        {
            try
            {
                await _userService.TransferUser(fromPickupLocationId, toPickupLocationId);
                return await Inventory();
            }
            catch(Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message}));
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(int id, string name, string address, decimal districtAdditionalRate, int sequence, string note)
        {
            try
            {
                await _userService.UpdatePickupLocation(id, name, address, districtAdditionalRate, sequence, note);
                return Json(new MethodResult<bool>(true));
            }
            catch(Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message}));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeletePickupLocation(id);
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string name, string address, decimal districtAdditionalRate, int sequence, int version, string latAndLng, int areaId, int? belongsToId, string note)
        {
            try
            {
                await _locationService.CreateAsync(new Domain.Entities.PickUpLocationEntity
                {
                    Name = name,
                    DetailArea = address,
                    DistrictAdditionalCost = districtAdditionalRate,
                    Number = sequence,
                    Version = version,
                    LatAndLng = latAndLng,
                    AreaId = areaId,
                    Note = note,
                    CompanyId = Config.COMPANY_ID,
                },
                belongsToId);
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                string message = e.Message;
                if (e.InnerException != null)
                {
                    message += ". " + e.InnerException.Message;
                }
                return Json(new MethodResult<bool>(new Error() { Text = message }));
            }
        }

        [HttpPost("new")]
        public async Task<IActionResult> New(string name, string address, decimal districtAdditionalRate, int sequence, int version, string latAndLng, int areaId, int? belongsToId, string note)
        {
            try
            {
                await _locationService.CreateAsync(new Domain.Entities.PickUpLocationEntity
                {
                    Name = name,
                    DetailArea = address,
                    DistrictAdditionalCost = districtAdditionalRate,
                    Number = sequence,
                    Version = version,
                    LatAndLng = latAndLng,
                    AreaId = areaId,
                    Note = note,
                    CompanyId = Config.COMPANY_ID,
                },
                belongsToId);
                return Json(new MethodResult<bool>(true));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<bool>(new Error() { Text = e.Message }));
            }
        }
    }
}
