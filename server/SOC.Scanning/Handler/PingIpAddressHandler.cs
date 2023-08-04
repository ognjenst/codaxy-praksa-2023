using Autofac.Core;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOC.Scanning.Services;
using System.Net;
using SOC.Scanning.Exceptions;

namespace SOC.Scanning.Handler;

internal class PingIpAddressRequest : IRequest<PingIpAddressResponse>
{
    [JsonProperty("ipAddress")]
    public string IpAddress { get; set; }
}

internal class PingIpAddressResponse
{
    [JsonProperty("isAvailable")]
    public bool IsAvailable { get; set; }
}

[OriginalName("SCANNING_ping_ip_address")]
internal class PingIpAddressHandler
    : ITaskRequestHandler<PingIpAddressRequest, PingIpAddressResponse>
{
    private readonly ILogger<PingIpAddressHandler> _logger;
    private readonly ISshClientService _sshClientService;

    public PingIpAddressHandler(ILogger<PingIpAddressHandler> logger, ISshClientService sshClientService)
    {
        _logger = logger;
        _sshClientService = sshClientService;
    }

    private bool ValidateInput(PingIpAddressRequest request)
    {
        if (!IPAddress.TryParse(request.IpAddress, out _))
            return false;
        return true;
    }

    public Task<PingIpAddressResponse> Handle(
        PingIpAddressRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = $"nmap {request.IpAddress}";
        
        if (!ValidateInput(request))
            throw new InvalidInputException("Input parameters are not valid!");

        _sshClientService.Connect();
        var result = _sshClientService.ExecuteCommand(command);

        if (!result.Result.Contains("Host is up"))
        {
            return Task.FromResult(new PingIpAddressResponse
            {
                IsAvailable = false
            }); ;
        }

        return Task.FromResult(new PingIpAddressResponse
        {
            IsAvailable = true
        }); ;

    }
}
