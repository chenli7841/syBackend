using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;

        public WarehouseController(IWarehouseService warehouseService, IMapper mapper)
        {
            _warehouseService = warehouseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Inventory()
        {
            var result = (await _warehouseService.ListAsync()).OrderBy(w => w.DisplaySequence);
            return View(result);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _warehouseService.GetAsync(id);
            var model = _mapper.Map<WarehouseViewModel>(result);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View("Edit", new WarehouseViewModel());
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _warehouseService.DeleteAsync(id);
            return RedirectToAction(nameof(Inventory));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(WarehouseViewModel model)
        {
            var warehouse = _mapper.Map<WarehouseEntity>(model);
            var result = await _warehouseService.SaveAsync(warehouse, model.PhotoData);
            return RedirectToAction(nameof(Edit), new {id = result.Id});
        }
    }
}
