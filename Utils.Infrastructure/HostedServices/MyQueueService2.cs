using Utils.Infrastructure.HostedServices.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Utils.Infrastructure.HostedServices
{
    public class MyQueueService2 : BackgroundService
    {
        private readonly IBackgroundTaskQueue<Guid> _queue;
        private readonly ILogger<MyQueueService1> _logger;

        public MyQueueService2(
            QueueService<Guid> queueService,
            ILogger<MyQueueService1> logger)
        {
            _queue = queueService.GetQueue("MyService2");
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"MyQueueService2 task doing background work.");
                var workItem = await _queue.DequeueAsync(stoppingToken);

                try
                {
                    var test = workItem;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"MyQueueService2 occurred error on {nameof(workItem)} with message {ex.Message}");
                }

                _logger.LogDebug($"MyQueueService2 background task is stopping.");
            }
        }
    }
}
