using IoT.Base.Infrastructure.Zigbee;

namespace IoT.Base.Infrastructure;

public interface IDeviceDescriptionManager
{
    internal void RegisterPayload(string deviceId, DeviceDescription payload);
    internal DeviceDescription GetPayload(string deviceId);
}
