using SOC.IoT.Base.Infrastructure.Zigbee;

namespace SOC.IoT.Base.Infrastructure;

public interface IDeviceDescriptionManager
{
    internal void RegisterPayload(string deviceId, DeviceDescription payload);
    internal DeviceDescription GetPayload(string deviceId);
}
