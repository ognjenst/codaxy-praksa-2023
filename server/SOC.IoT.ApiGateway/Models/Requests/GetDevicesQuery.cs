using MediatR;

namespace SOC.IoT.ApiGateway.Models.Requests
{
    public record GetDevicesQuery() : IRequest<IEnumerable<DeviceDTO>>;
}
