using AutoMapper;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SOC.IoT.Generated;
using SOC.IoT.Options;
using SOC.IoT.Services;

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
    private readonly IDevicesClient _devicesClient;
    private readonly ILogger<LightBulbColorChangeHandler> _logger;
    private readonly DeviceOptions _options;

    public LightBulbColorChangeHandler(
        IDevicesClient devicesClient,
        ILogger<LightBulbColorChangeHandler> logger,
        IOptions<DeviceOptions> options
    )
    {
        _devicesClient = devicesClient;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<NoOutput> Handle(
        ColorChangeRequest request,
        CancellationToken cancellationToken
    )
    {
        var device = await _devicesClient.GetDeviceAsync(request.DeviceId, cancellationToken);

        if (device is not null)
        {
            var deviceUpdate = new DeviceUpdateDTO
            {
                State = new DeviceState() { State = true },
                Light = new DeviceLight() { Brightness = _options.Brightness },
                ColorXy = new DeviceColorXy() { X = request.X, Y = request.Y }
            };

            // Light bulb turns on and off alternately <NumberOfRepetitions> times
            for (int i = 0; i < request.NumberOfRepetitions; i++)
            {
                try
                {
                    await _devicesClient.UpdateDeviceAsync(
                        request.DeviceId,
                        deviceUpdate,
                        cancellationToken
                    );
                    _logger.LogInformation("Light turned on.");
                    await Task.Delay(_options.DelayTime);

                    deviceUpdate.State.State = false;
                    await _devicesClient.UpdateDeviceAsync(
                        device.Id,
                        deviceUpdate,
                        cancellationToken
                    );
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
