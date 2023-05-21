using Utils.Infrastructure.HostedServices.Interfaces;
using System.Collections.Concurrent;

namespace Utils.Infrastructure.HostedServices
{
    public class QueueService<T>
    {
        private readonly ConcurrentDictionary<string, IBackgroundTaskQueue<T>> _queues;

        public QueueService()
        {
            _queues = new ConcurrentDictionary<string, IBackgroundTaskQueue<T>>();
        }

        public IBackgroundTaskQueue<T> GetQueue(string name)
        {
            return _queues.GetOrAdd(name, new BackgroundTaskQueue<T>());
        }
    }
}
