using MediatR;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.Base.Interfaces;

namespace SOC.IoT.ApiGateway.Handlers
{
    public class UpdateDeviceStateHandler : IRequestHandler<UpdateDeviceStateQuery>
    {
        private readonly IDeviceManager _deviceManager;
        public UpdateDeviceStateHandler(IDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }
        public async Task Handle(UpdateDeviceStateQuery request, CancellationToken cancellationToken)
        {
            await _deviceManager.SetDeviceStateAsync(request.payload.GetDevice(request.id));
        }
    }
}
