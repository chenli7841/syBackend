using EplusCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using WebUI.Models.ViewModels;
using WebUI.Models;
using Domain.Services;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using System.Linq;
using Common;

namespace EplusCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ISystemSession _systemSession;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ISystemSession systemSession)
        {
            _logger = logger;
            _userService = userService;
            _systemSession = systemSession;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.ListAsync(new UserListFilterOptions()
            {
                PageSize = int.MaxValue,
                RoleToSearch = Domain.Enums.RoleType.Admin
            });

            var result = new TodoItemInventoryResponse()
            {
                Users = users.Items,
                AdminUsers = users.Items.Where(u => u.Role == Domain.Enums.RoleType.Admin),
                CanDelete = _systemSession.CurrentUser.Id == 1 || _systemSession.CurrentUser.Id == 13
            };
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestStarAdmin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
