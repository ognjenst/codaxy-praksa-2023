using Newtonsoft.Json.Linq;
using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Parsers;

internal static class TemperatureParser
{
    public static DeviceTemperature? ParseTemperature(
        this DeviceDescription description,
        JObject payload
    )
    {
        var temperature = payload["temperature"];

        if (
            payload is null
            || temperature is null
            || !description.NumericValues.ContainsKey("temperature")
        )
            return null;

        var temperatureValue = description.NumericValues["temperature"];
        temperatureValue.SetValue(temperature.ToObject<decimal>()!);

        return new DeviceTemperature { Value = temperatureValue.Value };
    }
}
