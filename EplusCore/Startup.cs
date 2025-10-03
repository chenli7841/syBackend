using System;
using Common;
using Domain.Services;
using EplusCore.Middlewares;
using Infrastructure;
using Infrastructure.ChinaStatusService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Persistence.Data;
using Persistence.Services;
using WebUI.Helpers;
using WebUI.Middlewares;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<EplusDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection"), opt => opt.EnableRetryOnFailure()));

            services.AddDbContext<EplusDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("MySqlConnection_Test"),
                    new MySqlServerVersion(new Version(5, 5, 51))));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<EplusDbContext>();
            services.AddMemoryCache();

            services.AddHttpClient();
            services.AddAutoMapper(typeof(Startup));

            services.AddInfrastructure();
            services.AddPersistence();
            
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<ISmsService, SmsService>();
            services.AddScoped<ILogService, LogService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IChinaStatusService, ChinaStatusService>();
            services.AddTransient<IBatchService, BatchService>();
            services.AddTransient<IStatService, StatService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<ITodoItemService, TodoItemService>();

            var mvcBuilder = services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            #if DEBUG
                mvcBuilder.AddRazorRuntimeCompilation();
            #endif

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BasicAuthenticationHandler.SchemeName, null);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });
            
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthCodeMiddleware();
            app.UseSystemSessionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
