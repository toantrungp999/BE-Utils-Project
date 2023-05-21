namespace Utils.Infrastructure.HostedServices.Interfaces
{
    public interface IBackgroundTaskQueue<T>
    {
        ValueTask QueueBackgroundWorkItemAsync(T workItem);
        ValueTask<T> DequeueAsync(CancellationToken token);
    }
}
