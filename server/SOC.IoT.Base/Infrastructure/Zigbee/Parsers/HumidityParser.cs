using Newtonsoft.Json.Linq;
using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Parsers;

internal static class HumidityParser
{
    public static DeviceHumidity? ParseHumidity(this DeviceDescription description, JObject payload)
    {
        var humidity = payload["humidity"];

        if (
            payload is null
            || humidity is null
            || !description.NumericValues.ContainsKey("humidity")
        )
            return null;

        var humidityValue = description.NumericValues["humidity"];
        humidityValue.SetValue(humidity.ToObject<decimal>()!);

        return new DeviceHumidity { Value = humidityValue.Value };
    }
}
