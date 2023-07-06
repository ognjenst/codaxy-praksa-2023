using System.Text.Json.Serialization;

namespace IoT.Base.Infrastructure.Zigbee.Properties;

internal record EnumProperty
{
    [JsonPropertyName("values")]
    public IEnumerable<object> Values { get; init; } = Enumerable.Empty<object>();

    public string Name { get; init; }
}
