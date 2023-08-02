using AutoMapper;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SOC.IoT.Generated;
using SOC.IoT.Options;
using SOC.IoT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

[OriginalName("IoT_light_on_color_change")]
public class DetectionHandler : ITaskRequestHandler<DetectionRequest, NoOutput>
{
    private readonly IDeviceService _deviceService;

    public DetectionHandler(IDeviceService deviceService)
	{
        _deviceService = deviceService;
	}

	public async Task<NoOutput> Handle(DetectionRequest request, CancellationToken cancellationToken)
	{
		await _deviceService.LigthBulbInRepetitions(request, cancellationToken);
		return await Task.FromResult(new NoOutput());
	}
	
}
