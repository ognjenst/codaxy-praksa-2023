using Newtonsoft.Json;

namespace SOC.IoT.Base.Infrastructure.Zigbee;

internal record MqttDevice
{
    [JsonProperty("ieee_address")]
    public string IeeeAddress { get; init; }
    public Definition Definition { get; init; }
}

internal record Definition
{
    public string Description { get; init; }

    [JsonProperty("exposes")]
    public IEnumerable<Expose> Exposes { get; init; }
}

public record Expose
{
    public int? Access { get; init; }
    public string? Name { get; init; }
    public IEnumerable<Expose>? Features { get; init; }
    public string Type { get; init; }

    [JsonProperty("value_on")]
    public object? ValueOn { get; init; }

    [JsonProperty("value_off")]
    public object? ValueOff { get; init; }

    [JsonProperty("value_toggle")]
    public object? ValueToggle { get; init; }

    [JsonProperty("value_min")]
    public decimal? ValueMin { get; init; }

    [JsonProperty("value_max")]
    public decimal? ValueMax { get; init; }

    [JsonProperty("unit")]
    public string? Unit { get; init; }

    [JsonProperty("values")]
    public IEnumerable<object>? Values { get; init; }
}
