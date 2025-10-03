using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents
{
    public class UserRoutePermissionViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public UserRoutePermissionViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var userRoutes = await _userService.ListRouteAsync(userId);

            return View(userRoutes);
        }
    }
}