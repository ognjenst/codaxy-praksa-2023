using AutoMapper;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Newtonsoft.Json;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;

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

[OriginalName("IoT_device_update")]
public class DeviceHandler : ITaskRequestHandler<DeviceRequest, NoOutput>
{
    private readonly IMapper _mapper;
    private readonly IDeviceManager _deviceManager;

    public DeviceHandler(IMapper mapper, IDeviceManager deviceManager)
    {
        _mapper = mapper;
        _deviceManager = deviceManager;
    }

    public async Task<NoOutput> Handle(DeviceRequest request, CancellationToken cancellationToken)
    {
        var obj = _mapper.Map<Device>(request);

        var device = _deviceManager.GetDevice(request.Id);
        device.State = obj.State;
        device.ColorXy = obj.ColorXy;
        device.Light = obj.Light;
        await _deviceManager.SetDeviceStateAsync(device);

        return await Task.FromResult(new NoOutput());
    }
}
