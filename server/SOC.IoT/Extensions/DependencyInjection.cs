﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SOC.IoT.Handler;

namespace SOC.IoT.Extensions;
public static class DependencyInjection {
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddAutoMapper(typeof(Program).Assembly);

        services.RegisterOptions();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration().MinimumLevel
            .Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(loggingBuilder => {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        services.AddTransient<DeviceHandler>();

        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services) {
        //services.ConfigureOptions<SshOptionsSetup>();

        return services;
    }
}