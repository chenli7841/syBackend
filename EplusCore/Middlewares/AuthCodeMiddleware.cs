using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EplusCore.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthCodeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _authCode;

        public AuthCodeMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _authCode = config["AuthCode"];
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (!(httpContext.Request.Path.Value ?? "").StartsWith("/api/", StringComparison.OrdinalIgnoreCase) || httpContext.Request.Headers["AuthCode"] == _authCode)
            {
                return _next(httpContext);
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Task.FromResult(0);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthCodeMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthCodeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthCodeMiddleware>();
        }
    }
}
