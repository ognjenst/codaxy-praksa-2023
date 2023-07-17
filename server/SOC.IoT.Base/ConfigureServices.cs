using Microsoft.Extensions.DependencyInjection;
using SOC.IoT.Base.Infrastructure;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Base.Services;
using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base;

public static class ConfigureServices
{
    public static IServiceCollection AddIoTServices(this IServiceCollection services)
    {
        services.AddSingleton<IMqttService, MqttService>();
        services.AddSingleton<IZigbeeService, ZigbeeService>();
        services.AddSingleton<IDeviceDescriptionManager, DeviceDescriptionManager>();
        services.AddSingleton<IDeviceManager, DeviceManager>();
        services.AddSingleton<IEventQueueService<Device>, EventQueueService<Device>>();

        services.AddTransient<IStartupService, StartupService>();
        services.AddTransient<IDeviceWriter, DeviceWriter>();

        return services;
    }
}
