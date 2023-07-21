using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog.Events;
using Serilog;
using SOC.Notifications.Options;
using SOC.Notifications.OptionsSetup;
using SOC.Notifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SOC.Notifications.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.RegisterOptions();
            services.AddScoped<ISlackService, SlackService>();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel
            .Debug()
            .MinimumLevel
            .Override("Microsoft", LogEventLevel.Information)
            .WriteTo
            .Console()
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
            services.ConfigureOptions<SlackOptionsSetup>();

            return services;
        }
    }
}
