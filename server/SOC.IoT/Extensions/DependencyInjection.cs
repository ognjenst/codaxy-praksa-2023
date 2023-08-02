using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SOC.IoT.Generated;
using SOC.IoT.OptionsSetup;
using MediatR;
using SOC.IoT.Services;

namespace SOC.IoT.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterOptions();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		services.AddScoped<IDeviceService, DeviceService>();
		services.AddScoped<IDevicesClient, DevicesClient>();
        services.AddHttpClient<DevicesClient>();

        services.AddAutoMapper(typeof(Program).Assembly);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration().MinimumLevel
            .Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<DeviceOptionSetup>();

        return services;
    }
}
