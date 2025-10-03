using System.Linq;
using System.Threading.Tasks;
using Common;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebUI.Middlewares
{
    public class SystemSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SystemSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IUserService userService, ISystemSession systemSession)
        {
            UserEntity currentUser = null;
            var isAuthenticated = false;

            if (httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
            {
                isAuthenticated = true;
                currentUser = await userService.GetAsync(int.Parse(httpContext.User.Claims.First(c => c.Type == "UserId").Value));
            }

            systemSession.IsAuthenticated = isAuthenticated;
            systemSession.CurrentUser = currentUser;
            await _next(httpContext);
        }
    }

    public static class SystemSessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSystemSessionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SystemSessionMiddleware>();
        }
    }
}
