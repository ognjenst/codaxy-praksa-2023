using Microsoft.AspNetCore.SignalR;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.Base.Interfaces;
using System.Runtime.CompilerServices;

namespace SOC.IoT.ApiGateway.Hubs;

/// <summary>
/// Hub for device related events
/// </summary>
public class DevicesHub : Hub
{
    private readonly IDeviceManager _deviceManager;

    public DevicesHub(IDeviceManager deviceManager)
    {
        _deviceManager = deviceManager;
    }

    /// <summary>
    /// Streams any state updates of a single device
    /// </summary>
    /// <param name="deviceId">ID of the device</param>
    /// <returns>Stream of device states</returns>
    public async IAsyncEnumerable<DeviceDTO> DeviceStream(
        string deviceId,
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        await foreach (var item in _deviceManager.SubscribeAsync(deviceId, cancellationToken))
        {
            yield return new DeviceDTO(item);
        }
    }

    /// <summary>
    /// Streams any state updates of all devices
    /// </summary>
    /// <returns>Stream of device states</returns>
    public async IAsyncEnumerable<DeviceDTO> DevicesStream(
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        await foreach (var item in _deviceManager.SubscribeAllAsync(cancellationToken))
        {
            yield return new DeviceDTO(item);
        }
    }
}
