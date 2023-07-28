using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOC.IoT.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Handler;

public class TemperatureColorRequest : IRequest<TemperatureColorResponse>
{
    [JsonProperty("sensorId")]
    public string SensorId { get; set; }
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }
}

public class TemperatureColorResponse
{
    // [JsonProperty("message")]
    public string Message { get; set; }
}

[OriginalName("IoT_device_color_change")]
public class TemperatureColorHandler : ITaskRequestHandler<TemperatureColorRequest, TemperatureColorResponse>
{
    private readonly IDevicesClient _devicesClient;
    private readonly ILogger<TemperatureColorHandler> _logger;
    public TemperatureColorHandler(IDevicesClient devicesClient, ILogger<TemperatureColorHandler> logger)
    {
        _devicesClient = devicesClient;
        _logger = logger;
    }
    public async Task<TemperatureColorResponse> Handle(TemperatureColorRequest request, CancellationToken cancellationToken)
    {
        DeviceColorXy deviceColorXy;

        var sensor = await _devicesClient.GetDeviceAsync(request.SensorId, cancellationToken);
        var temperature = sensor.Temperature.Value;

        if (temperature >= 30) deviceColorXy = new DeviceColorXy() { X = 0.6942, Y = 0.2963 };
        else if (temperature >= 20) deviceColorXy = new DeviceColorXy() { X = 0.4339, Y = 0.5008 };
        else deviceColorXy = new DeviceColorXy() { X = 0.1355, Y = 0.0399 };

        var device = new DeviceUpdateDTO()
        {
            State = new DeviceState() { State = true },
            Light = new DeviceLight() { Brightness = 0.7 },
            ColorXy = deviceColorXy
        };

        try
        {
            await _devicesClient.UpdateDeviceAsync(request.DeviceId, device, cancellationToken);
            return new TemperatureColorResponse()
            {
                Message = $"Temperature on sensor {request.DeviceId} is {temperature} degrees Celsius."
            };
        } catch (Exception ex)
        {
            _logger.LogError($"Error while changing color of device {request.DeviceId}. {ex.Message}");
            return new TemperatureColorResponse() { Message = "" };
        }
    }
}
