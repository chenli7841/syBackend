using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.ViewModels;

namespace WebUI.ViewComponents
{
    public class OrderInfoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(OrderDetailViewModel order)
        {
            return View(order);
        }
    }
}
