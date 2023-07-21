using MediatR;

namespace SOC.IoT.ApiGateway.Models.Requests
{
    public record GetDeviceQuery(string id) : IRequest<DeviceDTO>;
}
