namespace SOC.Ticketing.Options;

public sealed class TicketingOptions
{
    public static readonly string SectionName = "TicketingOptions";

    public required string BaseUri { get; set; }
    public required string HiveApiKey { get; set; }
    
}
