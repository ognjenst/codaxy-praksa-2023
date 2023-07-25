using AutoMapper;
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

[OriginalName("IoT_device_update")]
public class DeviceHandler : ITaskRequestHandler<DeviceRequest, NoOutput>
{
    private readonly IDevicesClient _devicesClient;
    private readonly IMapper _mapper;

    public DeviceHandler(IDevicesClient devicesClient, IMapper mapper)
    {
        _devicesClient = devicesClient;
        _mapper = mapper;
    }

    public async Task<NoOutput> Handle(DeviceRequest request, CancellationToken cancellationToken)
    {
        var obj = _mapper.Map<DeviceUpdateDTO>(request);

        await _devicesClient.UpdateDeviceAsync(request.Id, obj, cancellationToken);

        return await Task.FromResult(new NoOutput());
    }
}
