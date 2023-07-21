using MediatR;
using Microsoft.EntityFrameworkCore;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.Base.Interfaces;

namespace SOC.IoT.ApiGateway.Handlers
{
    public class GetDevicesHandler : IRequestHandler<GetDevicesQuery, IEnumerable<DeviceDTO>>
    {
        private readonly SOCIoTDbContext _dbContext;
        private readonly IDeviceManager _deviceManager;
        public GetDevicesHandler(SOCIoTDbContext dbContext, IDeviceManager deviceManager)
        {
            _dbContext = dbContext;
            _deviceManager = deviceManager;
        }

        public async Task<IEnumerable<DeviceDTO>> Handle(GetDevicesQuery query, CancellationToken token)
        {
            var deviceDtos = _deviceManager.GetDevices().Select(d => new DeviceDTO(d)).ToList();
            var devices = await _dbContext.Set<Device>().ToListAsync(token);
            
            foreach (var deviceDto in deviceDtos)
            {
                foreach (var device in devices)
                {
                    if (device.IoTId == deviceDto.Id)
                    {
                        deviceDto.Name = device.Name;
                        deviceDto.Description = device.Description;
                        deviceDto.Manufacturer = device.Manufacturer;
                        deviceDto.Model = device.Model;
                        deviceDto.Type = device.Type.ToString();
                    }
                }
            }
            return deviceDtos;
        }
    }
}
