using Autofac.Core;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOC.Scanning.Services;
using System.Net;
using SOC.Scanning.Exceptions;
using static ConductorSharp.Engine.Service.DeploymentValidator;

namespace SOC.Scanning.Handler;

internal class ExamineNetworkScanRequest : IRequest<ExamineNetworkScanResponse>
{
    [JsonProperty("result")]
    public string Result { get; set; }
}

internal class ExamineNetworkScanResponse
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("severity")]
    public string Severity { get; set; }
}

[OriginalName("SCANNING_network_examiner")]
internal class ExamineNetworkScanHandler
    : ITaskRequestHandler<ExamineNetworkScanRequest, ExamineNetworkScanResponse>
{
    private readonly ILogger<ExamineNetworkScanHandler> _logger;

    public ExamineNetworkScanHandler(ILogger<ExamineNetworkScanHandler> logger)
    {
        _logger = logger;
    }

    public Task<ExamineNetworkScanResponse> Handle(
        ExamineNetworkScanRequest request,
        CancellationToken cancellationToken
    )
    {
        return Task.FromResult(new ExamineNetworkScanResponse
        {
            Message = request.Result,
            Title = "Network scan result",
            Severity = "LOW"
        });
    }
}
