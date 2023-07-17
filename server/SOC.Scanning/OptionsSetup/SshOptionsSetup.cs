using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SOC.Scanning.Options;

namespace SOC.Scanning.OptionsSetup;

internal sealed class SshOptionsSetup : IConfigureOptions<SshOptions>
{
    private readonly IConfiguration _configuration;

    public SshOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(SshOptions options) =>
        _configuration.GetSection(SshOptions.SectionName).Bind(options);
}
