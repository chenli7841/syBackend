using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Data;

namespace BackgroundService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // TODO: get connection string
                    services.AddDbContext<EplusDbContext>(options =>
                        options.UseMySql(
                            "",
                            new MySqlServerVersion(new Version(5, 5, 51))));

                    services.AddHostedService<Worker>();
                });
    }
}
