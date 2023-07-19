using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            string accessToken = configuration["SlackAccessToken"]; 
            string slackChannel = configuration["SlackChannel"];
            string slackContextBlockImageUrl = configuration["SlackContextBlockImageUrl"];
            services.AddScoped<ISlackService>(provider => new SlackService(accessToken, slackChannel, slackContextBlockImageUrl));

            return services;
        }

    }
}
