using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SOC.IoT.Options;
using SOC.IoT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Handler;

public class DetectionRequest : IRequest<NoOutput>
{
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }

    [JsonProperty("x")]
    public double X { get; set; }

    [JsonProperty("y")]
    public double Y { get; set; }

    [JsonProperty("startHour")]
    public int StartHour { get; set; }

    [JsonProperty("endHour")]
    public int EndHour { get; set; }

    [JsonProperty("maxNumberOfRepetitions")]
    public int MaxNumberOfRepetitions { get; set; }
}

[OriginalName("IoT_light_bulb_out_of_working_time")]
public class CheckTimeHandler : ITaskRequestHandler<DetectionRequest, NoOutput>
{
    private readonly IDeviceService _deviceService;

    public CheckTimeHandler(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    public async Task<NoOutput> Handle(
        DetectionRequest request,
        CancellationToken cancellationToken
    )
    {
        if (DateTime.Now.Hour <= request.StartHour && DateTime.Now.Hour >= request.EndHour)
        {
            await _deviceService.LigthBulbInRepetitions(request, cancellationToken);
        }
        return await Task.FromResult(new NoOutput());
    }
}
