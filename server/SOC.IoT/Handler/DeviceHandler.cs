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
    public DeviceState State { get; set; }

    [JsonProperty("light")]
    public DeviceLight Light { get; set; }

    [JsonProperty("colorXy")]
    public DeviceColorXy ColorXy { get; set; }
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
        await _devicesClient.UpdateDeviceAsync(request.Id, new DeviceUpdateDTO { State = request.State, ColorXy = request.ColorXy, Light = request.Light }, cancellationToken);

        return await Task.FromResult(new NoOutput());
    }
}
