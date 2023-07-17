using System.Text.Json.Serialization;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Properties;

internal record NumericProperty
{
    [JsonPropertyName("value_max")]
    public decimal? ValueMax { get; init; }

    [JsonPropertyName("value_min")]
    public decimal? ValueMin { get; init; }

    [JsonPropertyName("value_step")]
    public decimal? ValueStep { get; init; }

    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
}
