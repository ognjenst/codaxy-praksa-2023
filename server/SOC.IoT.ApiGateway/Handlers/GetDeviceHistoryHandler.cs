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

        public async Task<IEnumerable<DeviceHistoryDTO>> Handle(GetDeviceHistoryQuery request, CancellationToken cancellationToken)
        {
            var device = await _dbContext.Set<Device>().Where(d => d.IoTId == request.id).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception();
            var deviceHistory = await _dbContext.Set<DeviceHistory>().Where(h => h.DeviceID == device.Id).ToListAsync(cancellationToken);
            var deviceHistoryDtos = new List<DeviceHistoryDTO>();
            foreach (var history in deviceHistory)
            {
                deviceHistoryDtos.Add(new DeviceHistoryDTO { Time = history.Time, Configuration = history.Configuration });
            }
            return deviceHistoryDtos;
        }
    }
}
