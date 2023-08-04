using AutoMapper;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;
using SOC.IoT.Options;

namespace SOC.IoT.Handler;

public class ColorChangeRequest : IRequest<NoOutput>
{
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }

    [JsonProperty("x")]
    public double X { get; set; }

    [JsonProperty("y")]
    public double Y { get; set; }

    [JsonProperty("numberOfRepetitions")]
    public int NumberOfRepetitions { get; set; }
}

[OriginalName("IoT_light_on_color_change")]
public class LightBulbColorChangeHandler : ITaskRequestHandler<ColorChangeRequest, NoOutput>
{
    private readonly IDeviceManager _deviceManager;
    private readonly ILogger<LightBulbColorChangeHandler> _logger;
    private readonly DeviceOptions _options;

    public LightBulbColorChangeHandler(
        IDeviceManager deviceManager,
        ILogger<LightBulbColorChangeHandler> logger,
        IOptions<DeviceOptions> options
    )
    {
        _deviceManager = deviceManager;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<NoOutput> Handle(
        ColorChangeRequest request,
        CancellationToken cancellationToken
    )
    {
        var device = _deviceManager.GetDevice(request.DeviceId);

        if (device is not null)
        {
            device.State = new DeviceState { State = true };
            device.Light = new DeviceLight() { Brightness = (decimal)_options.Brightness };
            device.ColorXy = new DeviceColorXy { X = (decimal)request.X, Y = (decimal)request.Y };

            // Light bulb turns on and off alternately <NumberOfRepetitions> times
            for (int i = 0; i < request.NumberOfRepetitions; i++)
            {
                try
                {
                    device.State.State = true;
                    await _deviceManager.SetDeviceStateAsync(device);
                    _logger.LogInformation("Light turned on.");
                    await Task.Delay(_options.DelayTime);

                    device.State.State = false;
                    await _deviceManager.SetDeviceStateAsync(device);
                    _logger.LogInformation("Light turned off.");
                    await Task.Delay(_options.DelayTime);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        $"Error while changing color of device {request.DeviceId}. {ex.Message}"
                    );
                }
            }
        }
        return new NoOutput();
    }
}
