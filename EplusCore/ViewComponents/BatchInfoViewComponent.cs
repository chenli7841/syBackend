using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.ViewModels;

namespace WebUI.ViewComponents
{
    public class BatchInfoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(BatchViewModel batch)
        {
            return View(batch);
        }
    }
}
