using Autofac.Core;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOC.Scanning.Services;

namespace SOC.Scanning.Handler;

internal class ScanIpAddressRequest : IRequest<ScanIpAddressResponse>
{
    [JsonProperty("ipAddress")]
    public string IpAddress { get; set; }

    [JsonProperty("subnet")]
    public int Subnet { get; set; }
}

internal class ScanIpAddressResponse
{
    [JsonProperty("command")]
    public string Command { get; set; }

    [JsonProperty("result")]
    public string Result { get; set; }
}

[OriginalName("SCANNING_ip_address")]
internal class ScanIpAddressHandler
    : ITaskRequestHandler<ScanIpAddressRequest, ScanIpAddressResponse>
{
    private readonly ILogger<ScanIpAddressHandler> _logger;
    private readonly ISshClientService _sshClientService;

    public ScanIpAddressHandler(ILogger<ScanIpAddressHandler> logger, ISshClientService sshClientService)
    {
        _logger = logger;
        _sshClientService = sshClientService;
    }

    public Task<ScanIpAddressResponse> Handle(
        ScanIpAddressRequest request,
        CancellationToken cancellationToken
    )
    {
        _sshClientService.Connect();

        var command = $"nmap {request.IpAddress}";
        var result = _sshClientService.ExecuteCommand(command);

        return Task.FromResult(new ScanIpAddressResponse
        {
            Command=command, Result=result.Result
        });
    }
}
