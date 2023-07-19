using Autofac.Core;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SOC.Scanning.Handler;

internal class ScanIpAddressRequest : IRequest<ScanIpAddressResponse>
{
    public string IpAddress { get; set; }
    public int Subnet { get; set; }
}

internal class ScanIpAddressResponse
{
    public string NmapOutput { get; set; }
}

[OriginalName("SCANNING_ip_address")]
internal class ScanIpAddressHandler
    : ITaskRequestHandler<ScanIpAddressRequest, ScanIpAddressResponse>
{
    private readonly ILogger _logger;

    public ScanIpAddressHandler(ILogger<ScanIpAddressHandler> logger)
    {
        _logger = logger;
    }

    public Task<ScanIpAddressResponse> Handle(
        ScanIpAddressRequest request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
