using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;

namespace WebUI.Controllers
{
    public class DeliverProgressController : Controller
    {
        private readonly IDeliverProgressService _deliverProgressService;
        private readonly IRouteService _routeService;

        public DeliverProgressController(IDeliverProgressService deliverProgressService, IRouteService routeService)
        {
            _deliverProgressService = deliverProgressService;
            _routeService = routeService;
        }

        public async Task<IActionResult> Inventory()
        {
            var result = await _deliverProgressService.ListAsync();
            return View(result);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Routes = await _routeService.ListAsync();
            var result = await _deliverProgressService.GetAsync(id);
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Routes = await _routeService.ListAsync();
            return View("Edit", new DeliverProgressEntity());
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _deliverProgressService.DeleteAsync(id);
            return RedirectToAction(nameof(Inventory));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(DeliverProgressEntity model)
        {
            var result = await _deliverProgressService.SaveAsync(model);
            return RedirectToAction(nameof(Edit), new {id = result.Id});
        }
    }
}
