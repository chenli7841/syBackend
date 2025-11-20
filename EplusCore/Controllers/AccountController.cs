using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Services;
using EplusCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            var user = await _userService.GetAsync(model.UserName, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Login", model);
            }

            if (user.Role != RoleType.Admin && user.Role != RoleType.ChinaWarehouse && user.Role != RoleType.CanadaWarehouse && user.Role != RoleType.SubAdmin && user.Role != RoleType.SuperAdmin)
            {
                ModelState.AddModelError("", "Only Admin can log in.");
                return View("Login", model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Redirect(returnUrl ?? "~/Home/Index");
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
