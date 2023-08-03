using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOC.IoT.Generated;
using SOC.IoT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.IoT.Services;
using SOC.IoT.Base.Interfaces;

namespace SOC.IoT.Handler;

public class CheckIoTStatesRequest : IRequest<CheckIoTStatesResponse>
{
    [JsonProperty("percentageOfLoss")]
    public int PercentageOfLoss { get; set; }
}

public class CheckIoTStatesResponse
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("severity")]
    public string Severity { get; set; }

    [JsonProperty("incidentHappened")]
    public bool IncidentHappened { get; set; } 
}

[OriginalName("IoT_check_iot_states")]
public class CheckIoTStatesHandler : ITaskRequestHandler<CheckIoTStatesRequest, CheckIoTStatesResponse>
{
    private readonly IDeviceManager _deviceManager;
    private readonly ILogger<CheckIoTStatesHandler> _logger;
    private static List<string> _deviceIds = new List<string>()
    {
        "0x00178801062f573d", "0x00124b002268b3b2", "0x00124b0022d2d320", "0x04cf8cdf3c7597cd", "0x00124b00226969ac", "0xa4c13893cdd87674", "0x00158d0001dd7e46"
    };
    public CheckIoTStatesHandler(IDeviceManager deviceManager, ILogger<CheckIoTStatesHandler> logger)
    {
        _deviceManager = deviceManager;
        _logger = logger;
    }
    public async Task<CheckIoTStatesResponse> Handle(CheckIoTStatesRequest request, CancellationToken cancellationToken)
    {
        if(request.PercentageOfLoss >= 50) {
            return new CheckIoTStatesResponse()
            {
                Message = "IoT server is not available!",
                Title = "IoT state check",
                Severity = "HIGH",
                IncidentHappened = true
            };
        }

        string message = "";
        bool incidentHappened = false;

        foreach (var deviceId in _deviceIds)
        {
            var device = _deviceManager.GetDevice(deviceId);
            if(device == null)
            {
                incidentHappened = true;
                message += $"{deviceId} - Device is not available!\n";
            }
            else
                message += $"{deviceId} - Device is available.\n";

        }

        return new CheckIoTStatesResponse() {
            Message = message,
            Title = "IoT state check",
            Severity = incidentHappened ? "MEDIUM" : "LOW",
            IncidentHappened = incidentHappened
        };
        
    }
}
