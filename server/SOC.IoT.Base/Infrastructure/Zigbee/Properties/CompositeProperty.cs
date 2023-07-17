using Newtonsoft.Json;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Properties;

internal class CompositeProperty
{
    [JsonProperty("features")]
    public Expose[] Features { get; init; }
}
