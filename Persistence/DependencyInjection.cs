using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Services;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection));

            services.AddTransient<IRouteService, RouteService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IDeliverProgressService, DeliverProgressService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ISystemService, SystemService>();

            services.AddTransient<IAdminDataService, AdminDataService>();

            return services;
        }
    }
}
