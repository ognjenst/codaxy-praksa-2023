using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.Base.Infrastructure;
using SOC.IoT.Base.Interfaces;
using System.Threading;

namespace SOC.IoT.ApiGateway.Services
{
    public class DevicesBackgroundService : BackgroundService
    {
        private readonly IDeviceManager _deviceManager;
        private readonly ILogger<DevicesBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DevicesBackgroundService(IDeviceManager deviceManager, ILogger<DevicesBackgroundService> logger,IServiceScopeFactory serviceScopeFactory)
        {
             _deviceManager = deviceManager;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var item in _deviceManager.SubscribeAllAsync(stoppingToken))
            {
                var deviceHistory = new DeviceHistory
                {
                    Time = DateTime.UtcNow,
                    Configuration = JObject.FromObject(item, new Newtonsoft.Json.JsonSerializer() { ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() } })
                };
                SaveToDbAsync(deviceHistory, item);
                _logger.LogInformation("Device {@Device}", item);
            }
        }

        public async Task SaveToDbAsync(DeviceHistory deviceHistory, Domain.Entity.Device device)
        {
            using (var dbScope = _serviceScopeFactory.CreateScope())
            { 
                var dbContext = dbScope.ServiceProvider.GetRequiredService<SOCIoTDbContext>();
                int deviceId = dbContext.Devices.Where(x => x.IoTId == device.Id).Select(x => x.Id).FirstOrDefault();
                deviceHistory.DeviceID = deviceId;
                dbContext.Add(deviceHistory);
                dbContext.SaveChanges();
            }
        }
    }
}
