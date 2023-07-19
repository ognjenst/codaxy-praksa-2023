using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SOC.Notifications.Options;
using SOC.Notifications.OptionsSetup;
using SOC.Notifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddScoped<ISlackService>(provider =>
            {
                var slackOptions = provider.GetRequiredService<IOptions<SlackOptions>>().Value;
                return new SlackService(slackOptions);
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
