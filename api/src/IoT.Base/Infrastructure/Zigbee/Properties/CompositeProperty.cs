using Newtonsoft.Json;

namespace IoT.Base.Infrastructure.Zigbee.Properties;

internal class CompositeProperty
{
    [JsonProperty("features")]
    public Expose[] Features { get; init; }
}
