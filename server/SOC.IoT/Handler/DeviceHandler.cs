using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Newtonsoft.Json;
using SOC.IoT.Generated;
using System.ComponentModel.DataAnnotations;

namespace SOC.IoT.Handler;


public class DeviceRequest : IRequest<NoOutput>
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("state")]
    public bool State { get; set; }

    [JsonProperty("brightness")]
    public double Brightness { get; set; }

    [JsonProperty("x")]
    public double X { get; set; }
    [JsonProperty("y")]
    public double Y { get; set; }
}

[OriginalName("device_update_task")]
public class DeviceHandler : ITaskRequestHandler<DeviceRequest, NoOutput>
{
    private readonly IDevicesClient _devicesClient;

    public DeviceHandler(IDevicesClient devicesClient)
    {
        _devicesClient = devicesClient;
    }

    public async Task<NoOutput> Handle(DeviceRequest request, CancellationToken cancellationToken)
    {
        var obj = new DeviceUpdateDTO
        {
            State = new DeviceState { State = request.State },
            ColorXy = new DeviceColorXy { X = request.X, Y = request.Y },
            Light = new DeviceLight { Brightness = request.Brightness }
        };

        await _devicesClient.UpdateDeviceAsync(request.Id, obj, cancellationToken);

        return await Task.FromResult(new NoOutput());
    }
}
