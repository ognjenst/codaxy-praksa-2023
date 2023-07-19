using MediatR;
using SOC.IoT.ApiGateway.Entities;

namespace SOC.IoT.ApiGateway.Models.Requests
{
    public record GetDeviceHistoryQuery(string id) : IRequest<IEnumerable<DeviceHistoryDTO>>;
}
