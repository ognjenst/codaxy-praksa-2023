using IoT.Base.Infrastructure.Zigbee;
using IoT.Domain.Entity;
using Newtonsoft.Json.Linq;

namespace IoT.Base.Interfaces;

public interface IDeviceWriter
{
    public JObject GeneratePayload(Device device);
    internal Device ReadDevice(DeviceDescription description, JObject payload);
}
