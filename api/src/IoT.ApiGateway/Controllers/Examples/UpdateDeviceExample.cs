using IoT.ApiGateway.Models;
using Swashbuckle.AspNetCore.Filters;

namespace IoT.ApiGateway.Controllers.Examples;

public class UpdateDeviceExample : IExamplesProvider<DeviceUpdateDTO>
{
    public DeviceUpdateDTO GetExamples() =>
        new DeviceUpdateDTO
        {
            Light = new Domain.Entity.DeviceLight { Brightness = 0.44m },
            State = new Domain.Entity.DeviceState { State = true },
            ColorXy = new Domain.Entity.DeviceColorXy { X = 0.44m, Y = 0.71m }
        };
}
