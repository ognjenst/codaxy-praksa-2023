using MediatR;

namespace SOC.IoT.ApiGateway.Models.Requests
{
    public record UpdateDeviceStateQuery(string id, DeviceUpdateDTO payload) : IRequest;
}
