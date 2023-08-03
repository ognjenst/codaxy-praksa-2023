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

    [JsonProperty("numberOfRequests")]
    public int NumberOfRequests { get; set; }
}

internal class PingIpAddressResponse
{
    [JsonProperty("command")]
    public string Command { get; set; }

    [JsonProperty("result")]
    public string Result { get; set; }

    [JsonProperty("percentageOfLoss")]
    public int PercentageOfLoss { get; set; }
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
        var command = $"ping -c {request.NumberOfRequests} {request.IpAddress}";
        
        if (!ValidateInput(request))
            throw new InvalidInputException("Input parameters are not valid!");

        _sshClientService.Connect();
        var result = _sshClientService.ExecuteCommand(command);

        int startIndex = result.Result.IndexOf("received") + 10;
        int endIndex = result.Result.IndexOf("packet loss") - 2;
        string percentage = result.Result.Substring(startIndex, endIndex - startIndex);
        int percentageOfLoss = int.Parse(percentage);

        return Task.FromResult(new PingIpAddressResponse
        {
            Command = command,
            Result = result.Result,
            PercentageOfLoss = percentageOfLoss
        });

    }
}
