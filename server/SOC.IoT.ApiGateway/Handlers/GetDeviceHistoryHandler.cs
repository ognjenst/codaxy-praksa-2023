using MediatR;
using Microsoft.EntityFrameworkCore;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.Base.Interfaces;

namespace SOC.IoT.ApiGateway.Handlers
{
    public class GetDeviceHistoryHandler : IRequestHandler<GetDeviceHistoryQuery, IEnumerable<DeviceHistoryDTO>>
    {
        private readonly SOCIoTDbContext _dbContext;
        public GetDeviceHistoryHandler(SOCIoTDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DeviceHistoryDTO>?> Handle(GetDeviceHistoryQuery request, CancellationToken cancellationToken)
        {
            var device = await _dbContext.Devices.Include(d => d.DevicesHistory).Where(d => d.IoTId == request.id).FirstOrDefaultAsync(cancellationToken);
            if (device is null)
            {
                return null;
            }
            var deviceHistory = device.DevicesHistory;
            var deviceHistoryDtos = new List<DeviceHistoryDTO>();
            foreach (var history in deviceHistory)
            {
                deviceHistoryDtos.Add(new DeviceHistoryDTO { Time = history.Time, Configuration = history.Configuration.ToString() });
            }
            return deviceHistoryDtos;
        }
    }
}
