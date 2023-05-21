using Utils.Infrastructure.HostedServices.Interfaces;
using System.Collections.Concurrent;

namespace Utils.Infrastructure.HostedServices
{
    public class BackgroundTaskQueue<T> : IBackgroundTaskQueue<T>
    {
        private readonly ConcurrentQueue<T> _workItems = new ConcurrentQueue<T>();
        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);
        public async ValueTask<T> DequeueAsync(CancellationToken token)
        {
            await _signal.WaitAsync(token);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }

        public async ValueTask QueueBackgroundWorkItemAsync(T workItem)
        {
            if (workItem is null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            await Task.Run(() =>
            {
                _workItems.Enqueue(workItem);
            });

            _signal.Release();
        }
    }
}
