using Microsoft.Extensions.Options;
using SOC.Conductor.Options;

namespace SOC.Conductor.OptionsSetup;

public class ConductorOptionsSetup : IConfigureOptions<ConductorOptions>
{
    private readonly IConfiguration _configuration;

    public ConductorOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(ConductorOptions options)
    {
        _configuration.GetSection(ConductorOptions.SectionName).Bind(options);
    }
}
