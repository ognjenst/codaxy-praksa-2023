using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SOC.Ticketing.Options;

namespace SOC.Ticketing.OptionsSetup;

internal sealed class TicketingOptionsSetup : IConfigureOptions<TicketingOptions>
{
    private readonly IConfiguration _configuration;

    public TicketingOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(TicketingOptions options) =>
        _configuration.GetSection(TicketingOptions.SectionName).Bind(options);
}
