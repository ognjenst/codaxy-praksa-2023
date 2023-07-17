using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SOC.IoT.Base.Infrastructure;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace SOC.IoT.Base.Services;

public class DeviceManager : IDeviceManager
{
    private readonly IEventQueueService<Device> _queue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IDeviceWriter _writer;
    private readonly ILogger<DeviceManager> _logger;
    private readonly ConcurrentDictionary<Guid, CancellationTokenSource> _callbackRegistry = new();

    private readonly ConcurrentDictionary<string, Device> _devices = new();

    public DeviceManager(
        IEventQueueService<Device> queue,
        IServiceScopeFactory scopeFactory,
        IDeviceWriter writer,
        ILogger<DeviceManager> logger
    )
    {
        _queue = queue;
        _scopeFactory = scopeFactory;
        _writer = writer;
        _logger = logger;
    }

    public Guid AttachListener(string deviceId, Func<Device, Task> listener)
    {
        var cts = new CancellationTokenSource();
        var id = Guid.NewGuid();

        Task.Run(async () =>
        {
            await foreach (var item in SubscribeAsync(deviceId, cts.Token))
            {
                if (cts.IsCancellationRequested)
                    return;

                await listener(item);
            }
        });

        _callbackRegistry.TryAdd(id, cts);

        return id;
    }

    public void DetachListener(Guid listenerId)
    {
        if (!_callbackRegistry.TryRemove(listenerId, out var cts))
        {
            throw new KeyNotFoundException(nameof(listenerId));
        }

        cts.Cancel();
    }

    public Device GetDevice(string deviceId) => _devices[deviceId];

    public IEnumerable<Device> GetDevices() => _devices.Values;

    public async Task SetDeviceStateAsync(Device device)
    {
        using var scope = _scopeFactory.CreateScope();
        var mqtt = scope.ServiceProvider.GetRequiredService<IMqttService>();

        var payload = _writer.GeneratePayload(device);

        if (payload is null)
        {
            _logger.LogError($"Unable to generate payload for device {device.Id}");
            return;
        }

        await mqtt.PublishAsync($"zigbee2mqtt/{device.Id}/set", payload);
    }

    public IAsyncEnumerable<Device> SubscribeAllAsync(CancellationToken cancellationToken) =>
        _queue.SubscribeAsync(cancellationToken);

    public async IAsyncEnumerable<Device> SubscribeAsync(
        string deviceId,
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        await foreach (var item in _queue.SubscribeAsync(cancellationToken))
        {
            if (item.Id != deviceId)
                continue;

            yield return item;
        }
    }

    void IDeviceManager.UpdateDeviceState(Device device)
    {
        _devices.AddOrUpdate(
            device.Id,
            device,
            (key, old) =>
            {
                if (device.Capabilities is null || device.Capabilities.Count is 0)
                {
                    device.Capabilities = old.Capabilities;
                }

                return device;
            }
        );
        _queue.Publish(device);
    }
}
