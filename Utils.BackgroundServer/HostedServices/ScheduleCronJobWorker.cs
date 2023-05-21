using Utils.Application.Services.Interfaces;
using Utils.Infrastructure.HostedServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utils.BackgroundServer.HostedServices
{
    public class ScheduleCronJobWorker : CronJobBackgroundService
    {
        private readonly ILogger<ScheduleCronJobWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ScheduleCronJobWorker(
            ILogger<ScheduleCronJobWorker> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            Cron = "0 0/1 * 1/1 * ? *"; // every minute
        }

        protected override async Task DoWork(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Worker running at : {Time}", DateTimeOffset.Now);

                using var scope = _serviceProvider.CreateScope();
                var testService = scope.ServiceProvider.GetRequiredService<ITestService>();
                await testService.AddTest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
