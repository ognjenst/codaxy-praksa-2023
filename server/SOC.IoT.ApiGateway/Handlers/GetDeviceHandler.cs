using MediatR;
using Microsoft.EntityFrameworkCore;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.Base.Interfaces;

namespace SOC.IoT.ApiGateway.Handlers
{
    public class GetDeviceHandler : IRequestHandler<GetDeviceQuery, DeviceDTO>
    {
        private readonly SOCIoTDbContext _dbContext;
        private readonly IDeviceManager _deviceManager;
        public GetDeviceHandler(SOCIoTDbContext dbContext, IDeviceManager deviceManager)
        {
            _dbContext = dbContext;
            _deviceManager = deviceManager;
        }
    
        public async Task<DeviceDTO> Handle(GetDeviceQuery request, CancellationToken cancellationToken)
        {
            var deviceDto = new DeviceDTO(_deviceManager.GetDevice(request.id));
            var device = await _dbContext.Set<Device>().Where(d => d.IoTId == request.id).FirstOrDefaultAsync(cancellationToken);
            deviceDto.Name = device.Name;
            deviceDto.Description = device.Description;
            deviceDto.Manufacturer = device.Manufacturer;
            deviceDto.Model = device.Model;
            deviceDto.Type = device.Type.ToString();
            return deviceDto;
        }
    }
}
