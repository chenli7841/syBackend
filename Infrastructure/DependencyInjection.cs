using Common;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISystemSession, SystemSession>();
            services.AddTransient<IDateTime, EplusDateTime>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IFileExportService, FileExportService>();
            services.AddTransient<INotificationService, NotificationService>();

            return services;
        }
    }
}
