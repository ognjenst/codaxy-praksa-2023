using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;
using SOC.IoT.Models;
using DeviceColorXy = SOC.IoT.Domain.Entity.DeviceColorXy;

namespace SOC.IoT.Handler;

public class TemperatureColorRequest : IRequest<TemperatureColorResponse>
{
    [JsonProperty("sensorId")]
    public string? SensorId { get; set; }

    [JsonProperty("deviceId")]
    public string? DeviceId { get; set; }
}

public class TemperatureColorResponse
{
    public string? Message { get; set; }
}

[OriginalName("IoT_device_color_change")]
public class TemperatureColorHandler
    : ITaskRequestHandler<TemperatureColorRequest, TemperatureColorResponse>
{
    private readonly IDeviceManager _deviceManager;
    private readonly ILogger<TemperatureColorHandler> _logger;

    public TemperatureColorHandler(
        IDeviceManager deviceManager,
        ILogger<TemperatureColorHandler> logger
    )
    {
        _deviceManager = deviceManager;
        _logger = logger;
    }

    public async Task<TemperatureColorResponse> Handle(
        TemperatureColorRequest request,
        CancellationToken cancellationToken
    )
    {
        DeviceColorXy deviceColorXy;

        var sensor = _deviceManager.GetDevice(request.SensorId);
        var temperature = sensor.Temperature!.Value;

        if (temperature >= 30)
            deviceColorXy = Colors.RED;
        else if (temperature >= 20)
            deviceColorXy = Colors.YELLOW;
        else
            deviceColorXy = Colors.BLUE;

        var device = _deviceManager.GetDevice(request.DeviceId);

        device.State = new DeviceState { State = true };
        device.ColorXy = deviceColorXy;

        try
        {
            await _deviceManager.SetDeviceStateAsync(device);
            return new TemperatureColorResponse()
            {
                Message =
                    $"Temperature on sensor {request.DeviceId} is {temperature} degrees Celsius."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(
                $"Error while changing color of device {request.DeviceId}. {ex.Message}"
            );
            return new TemperatureColorResponse() { Message = "" };
        }
    }
}
