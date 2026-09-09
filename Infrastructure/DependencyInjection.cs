using Common;
using Common.WeCom;
using Infrastructure.WeCom;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, WeComOptions weComOptions)
        {
            services.AddScoped<ISystemSession, SystemSession>();
            services.AddTransient<IDateTime, EplusDateTime>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IFileExportService, FileExportService>();
            services.AddTransient<INotificationService, NotificationService>();

            services.AddSingleton(weComOptions);
            services.AddHttpClient(nameof(WeComApiClient), client =>
            {
                var baseUrl = weComOptions.ApiBaseUrl ?? "https://qyapi.weixin.qq.com";
                client.BaseAddress = new System.Uri(baseUrl.TrimEnd('/'));
                client.Timeout = System.TimeSpan.FromSeconds(15);
            });
            services.AddSingleton<IWeComApiClient, WeComApiClient>();
            services.AddSingleton<IWeComCallbackCrypt, WeComCallbackCrypt>();

            return services;
        }
    }
}
