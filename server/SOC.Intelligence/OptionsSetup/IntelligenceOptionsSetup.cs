using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SOC.Intelligence.Options;

namespace SOC.Intelligence.OptionsSetup;

internal sealed class IntelligenceOptionsSetup : IConfigureOptions<IntelligenceOptions>
{
	private readonly IConfiguration _configuration;

    public IntelligenceOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(IntelligenceOptions options) =>
        _configuration.GetSection(IntelligenceOptions.SectionName).Bind(options);
}
