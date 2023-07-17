using SOC.IoT.Base.Interfaces;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace SOC.IoT.Base.Services;

internal class EventQueueService<T> : IEventQueueService<T>
{
    private readonly HashSet<Consumer> _consumers = new();

    public async IAsyncEnumerable<T> SubscribeAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        var consumer = new Consumer();
        try
        {
            _consumers.Add(consumer);

            while (!cancellationToken.IsCancellationRequested)
            {
                await consumer.Source.Task;
                while (consumer.Queue.TryDequeue(out var eventRecord))
                {
                    yield return eventRecord;
                }
                consumer.Source = new();
            }
        }
        finally
        {
            _consumers.Remove(consumer);
        }
    }

    public void Publish(T message)
    {
        foreach (var consumer in _consumers)
        {
            consumer.Queue.Enqueue(message);
            consumer.Source.TrySetResult();
        }
    }

    private record Consumer
    {
        public TaskCompletionSource Source = new();
        public readonly ConcurrentQueue<T> Queue = new();

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
