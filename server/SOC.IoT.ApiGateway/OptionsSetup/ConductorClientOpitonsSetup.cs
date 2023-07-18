using Microsoft.Extensions.Options;
using SOC.IoT.ApiGateway.Options;

namespace SOC.IoT.ApiGateway.OptionsSetup;

public class ConductorClientOpitonsSetup : IConfigureOptions<ConductorClientOpitons>
{
    private readonly IConfiguration _configuration;

    public ConductorClientOpitonsSetup(IConfiguration configuration) =>
        _configuration = configuration;

    public void Configure(ConductorClientOpitons options) =>
        _configuration.GetSection(ConductorClientOpitons.SectionName).Bind(options);
}
