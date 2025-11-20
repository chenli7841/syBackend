using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;

        public RouteController(IRouteService routeService, IWarehouseService warehouseService, IMapper mapper)
        {
            _routeService = routeService;
            _warehouseService = warehouseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Inventory()
        {
            var routes = (await _routeService.ListAsync()).OrderBy(r => r.DisplaySequence);
            return View(routes);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Warehouses = await _warehouseService.ListAsync();
            var route = await _routeService.GetAsync(id);
            var result = _mapper.Map<RouteViewModel>(route);
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Warehouses = await _warehouseService.ListAsync();
            var result = _mapper.Map<RouteViewModel>(new RouteEntity());
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
