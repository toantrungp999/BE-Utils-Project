using Utils.Application.Services.Interfaces;
using Utils.Infrastructure.HostedServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Utils.Infrastructure.HostedServices
{
    public class MyQueueService1 : BackgroundService
    {
        private readonly IBackgroundTaskQueue<Guid> _queue;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MyQueueService1> _logger;

        public MyQueueService1(
            QueueService<Guid> queueService,
            IServiceProvider serviceProvider,
            ILogger<MyQueueService1> logger)
        {
            _queue = queueService.GetQueue("MyService1");
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"MyQueueService1 task doing background work.");
                var workItem = await _queue.DequeueAsync(stoppingToken);

                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var testService = scope.ServiceProvider.GetRequiredService<ITestService>();
                    await testService.AddTest(workItem);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"MyQueueService1 occurred error on {nameof(workItem)} with message {ex.Message}");
                }

                _logger.LogDebug($"MyQueueService1 background task is stopping.");
            }
        }
    }
}
