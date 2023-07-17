using SOC.IoT.ApiGateway.Models;
using SOC.IoT.Domain.Entity;
using SOC.IoT.Domain.Enum;
using Swashbuckle.AspNetCore.Filters;

namespace SOC.IoT.ApiGateway.Controllers.Examples;

public class DeviceExample : IExamplesProvider<DeviceDTO>
{
    public DeviceDTO GetExamples() =>
        new DeviceDTO(
            new Device
            {
                Id = "0xfedcba9876543210",
                Capabilities = new[]
                {
                    DeviceCapability.State,
                    DeviceCapability.Light,
                    DeviceCapability.ColorXy
                },
                State = new DeviceState { State = true }
            }
        );
}
