using Microsoft.Extensions.Options;
using SOC.IoT.ApiGateway.Options;

namespace SOC.IoT.ApiGateway.OptionsSetup
{
    public sealed class JwtSecretSetup : IConfigureOptions<JwtSecret>
    {
        private readonly IConfiguration _configuration;

        public JwtSecretSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(JwtSecret options) =>
            _configuration.GetSection(JwtSecret.SectionName).Bind(options);
    }
}
