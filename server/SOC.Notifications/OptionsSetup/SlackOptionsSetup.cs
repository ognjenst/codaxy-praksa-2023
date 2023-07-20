using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SOC.Notifications.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Notifications.OptionsSetup
{
    internal sealed class SlackOptionsSetup : IConfigureOptions<SlackOptions>
    {
        private readonly IConfiguration _configuration;

        public SlackOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(SlackOptions options) => _configuration.GetSection(SlackOptions.SectionName).Bind(options);
    }
}
