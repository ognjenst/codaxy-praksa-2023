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

public class DetectionResponse
{
	[JsonProperty("message")]
	public string Message { get; set; }
	[JsonProperty("title")]
	public string Title { get; set; }
	[JsonProperty("severity")]
	public string Severity { get; set; }
}

public class DetectionRequest : IRequest<DetectionResponse> 
{
    [JsonProperty("deviceId")]
    public string DeviceId { get; set; }
	[JsonProperty("x")]
	public double X { get; set; }
	[JsonProperty("y")]
	public double Y { get; set; }
}

[OriginalName("IoT_light_on_color_change")]
public class DetectionHandler : ITaskRequestHandler<DetectionRequest, DetectionResponse>
{
    private readonly IDeviceService _deviceService;

    public DetectionHandler(IDeviceService deviceService)
	{
        _deviceService = deviceService;
	}

	public async Task<DetectionResponse> Handle(DetectionRequest request, CancellationToken cancellationToken)
	{
		await _deviceService.LigthBulbInRepetitions(request, cancellationToken);
		return await Task.FromResult(new DetectionResponse
		{
			Message = "MOVEMENT DETECTED OUTSIDE OF WORKING HOURS",
			Title = "MOVEMENT DETECTION",
			Severity = "HIGH"
		});
	}
}
