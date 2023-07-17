using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base.Interfaces;

/// <summary>
/// Device manager allows monitoring and state manipulation of devices in real time.
/// </summary>
public interface IDeviceManager
{
    /// <summary>
    /// Returns every device with its last known state.
    /// </summary>
    public IEnumerable<Device> GetDevices();

    /// <summary>
    /// Returns a single device with its last known state
    /// </summary>
    /// <param name="deviceId">Device ID</param>
    /// <returns><see cref="Device">Device</see> object with at least the Id and Capabilities fields populated</returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public Device GetDevice(string deviceId);

    /// <summary>
    /// Exposes an awaitable enumerator that yields results on every provided device change.
    /// </summary>
    /// <param name="deviceId">ID of the device</param>
    /// <param name="cancellationToken">Cancellation token that will close the stream</param>
    /// <exception cref="KeyNotFoundException"></exception>
    public IAsyncEnumerable<Device> SubscribeAsync(
        string deviceId,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Exposes an awaitable enumerator that yields results on every change.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token that will close the stream</param>
    /// <exception cref="KeyNotFoundException"></exception>
    public IAsyncEnumerable<Device> SubscribeAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Attaches a callback function that will be executed every time there is an update on the specified device.
    /// </summary>
    /// <param name="deviceId">ID of the device</param>
    /// <param name="listener">Listener task taht will be executed on every update</param>
    /// <returns>Unique ID of the listener which can be cancelled with <see cref="DetachListener(Guid)">DetachListener</see>.</returns>
    public Guid AttachListener(string deviceId, Func<Device, Task> listener);

    /// <summary>
    /// Detaches a listener created with <see cref="AttachListener(string, Func{Device, Task})">DetachListener.</see>
    /// </summary>
    /// <param name="listenerId">ID of the attached listener.</param>
    /// <exception cref="KeyNotFoundException"></exception>
    public void DetachListener(Guid listenerId);

    /// <summary>
    /// Sets the state of a device.
    /// </summary>
    /// <param name="device">Device with the new state that will be set.</param>
    /// <exception cref="KeyNotFoundException"></exception>
    public Task SetDeviceStateAsync(Device device);

    internal void UpdateDeviceState(Device device);
}
