using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace BackgroundService
{
    public class Worker : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public IServiceProvider Services { get; }

        public Worker(ILogger<Worker> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Console.Out.WriteLineAsync($"test: {DateTimeOffset.Now}");

                using (var scope = Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<EplusDbContext>();
                    var order = await context.TransportOrders.FirstAsync(o => o.OrderNumber == "H47110099498081", stoppingToken);
                    order.Memo = $"Updated at {DateTimeOffset.UtcNow} (UTC)";
                    await context.SaveChangesAsync(stoppingToken);

                    _logger.LogInformation("New memo: {memo}", order.Memo);
                }

                await Task.Delay(1000 * 60 * 60, stoppingToken);
            }
        }
    }
}
