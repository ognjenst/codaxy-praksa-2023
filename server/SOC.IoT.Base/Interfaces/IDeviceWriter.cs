using Newtonsoft.Json.Linq;
using SOC.IoT.Base.Infrastructure.Zigbee;
using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base.Interfaces;

public interface IDeviceWriter
{
    public JObject GeneratePayload(Device device);
    internal Device ReadDevice(DeviceDescription description, JObject payload);
}
