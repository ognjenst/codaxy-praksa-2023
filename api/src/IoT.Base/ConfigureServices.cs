using IoT.Base.Infrastructure;
using IoT.Base.Interfaces;
using IoT.Base.Services;
using IoT.Domain.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.Base;

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
