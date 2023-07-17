using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Util;
using MediatR;

namespace SOC.Scanning.Handler;

internal class ScanIpAdrressRequest : IRequest<ScanIpAdrressResponse>
{
    public string IpAddress { get; set; }
    public int Subnet { get; set; }
}

internal class ScanIpAdrressResponse
{
    public string NmapOutput { get; set; }
}

[OriginalName("SCANNING_ip_address")]
internal class ScanIpAdrressHandler
    : ITaskRequestHandler<ScanIpAdrressRequest, ScanIpAdrressResponse>
{
    public Task<ScanIpAdrressResponse> Handle(
        ScanIpAdrressRequest request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
