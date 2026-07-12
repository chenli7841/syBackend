using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;
        private readonly EplusDbContext _context;
        private readonly ISystemService _systemService;

        public RouteController(IRouteService routeService, IWarehouseService warehouseService, IMapper mapper, EplusDbContext context, ISystemService systemService)
        {
            _routeService = routeService;
            _warehouseService = warehouseService;
            _mapper = mapper;
            _context = context;
            _systemService = systemService;
        }

        public async Task<IActionResult> Inventory(string companyIds)
        {
            ViewBag.Companies = await _systemService.GetSelectableCompaniesAsync();
            var parsedCompanyIds = await _systemService.ResolveCompanyIdsAsync(companyIds);
            var routes = (await _routeService.ListAsync(parsedCompanyIds.Length == 0 ? null : parsedCompanyIds)).OrderBy(r => r.DisplaySequence);
            return View(routes);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Warehouses = await _warehouseService.ListAsync();
            var companies = await _systemService.GetSelectableCompaniesAsync();
            ViewBag.Companies = companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            var route = await _routeService.GetAsync(id, false);
            var result = _mapper.Map<RouteViewModel>(route);
            var lockedCompanyId = (await _systemService.GetSettingsAsync()).LockedCompanyId;
            if (lockedCompanyId.HasValue)
            {
                result.CompanyId = lockedCompanyId.Value;
            }
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Warehouses = await _warehouseService.ListAsync();
            var companies = await _systemService.GetSelectableCompaniesAsync();
            ViewBag.Companies = companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            var result = _mapper.Map<RouteViewModel>(new RouteEntity());
            var lockedCompanyId = (await _systemService.GetSettingsAsync()).LockedCompanyId;
            if (lockedCompanyId.HasValue)
            {
                result.CompanyId = lockedCompanyId.Value;
            }
            return View("Edit", result);
        }

        public async Task<IActionResult> Hide(int id)
        {
            await _routeService.HideAsync(id);
            return RedirectToAction(nameof(Inventory));
        }

        public async Task<IActionResult> Show(int id)
        {
            await _routeService.ShowAsync(id);
            return RedirectToAction(nameof(Inventory));
        }

        public async Task<IActionResult> ToggleIsRegular(int id)
        {
            await _routeService.ToggleIsRegular(id);
            return RedirectToAction(nameof(Inventory));
        }

        public async Task<IActionResult> RemovePermissionsAsync(int id)
        {
            await _routeService.RemovePermissionsAsync(id);
            return RedirectToAction(nameof(Edit), new {id});
        }

        public async Task<IActionResult> AddAllPermissionsAsync(int id)
        {
            await _routeService.AddAllPermissionsAsync(id);
            return RedirectToAction(nameof(Edit), new {id});
        }

        public async Task<IActionResult> Permissions(int routeId)
        {
            var result = await _routeService.ListPermissionsAsync(routeId);
            return View(result);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(RouteViewModel model)
        {
            var route = _mapper.Map<RouteEntity>(model);
            var result = await _routeService.SaveAsync(route, model.PhotoData);
            return RedirectToAction(nameof(Edit), new {id = result.Id});
        }

        [HttpPost]
        public async Task<JsonResult> GetRouteImageUploadUrl(int routeId)
        {
            try
            {
                var imageUploadUrl = await _routeService.GetRouteImageUploadUrl(routeId);
                return Json(new MethodResult<string>(imageUploadUrl));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<SystemPhotoEntity>(new Error() { Text = e.Message }));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _routeService.DeleteAsync(id);
            return RedirectToAction(nameof(Inventory));
        }
        public async Task<IEnumerable<RouteEntity>> List()
        {
            return await _routeService.ListAsync();
        }
    }
}
