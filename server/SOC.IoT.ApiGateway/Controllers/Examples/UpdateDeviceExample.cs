using SOC.IoT.ApiGateway.Models;
using SOC.IoT.Domain.Entity;
using Swashbuckle.AspNetCore.Filters;

namespace SOC.IoT.ApiGateway.Controllers.Examples;

public class UpdateDeviceExample : IExamplesProvider<DeviceUpdateDTO>
{
    public DeviceUpdateDTO GetExamples() =>
        new DeviceUpdateDTO
        {
            Light = new DeviceLight { Brightness = 0.44m },
            State = new DeviceState { State = true },
            ColorXy = new DeviceColorXy { X = 0.44m, Y = 0.71m }
        };
}
