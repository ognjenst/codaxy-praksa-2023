using Newtonsoft.Json.Linq;
using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Parsers;

internal static class ColorXyParser
{
    public static DeviceColorXy? ParseColorXy(this DeviceDescription description, JObject payload)
    {
        var color = payload["color"];

        if (
            payload is null || color is null || !description.CompositeValues.ContainsKey("color_xy")
        )
            return null;

        var colorX = description.CompositeValues["color_xy"].NumericValues["x"];
        var colorY = description.CompositeValues["color_xy"].NumericValues["y"];

        if (colorX is null || colorY is null || color["x"] is null || color["y"] is null)
            return null;

        colorX.SetValue(color["x"]!.ToObject<decimal>());
        colorY.SetValue(color["y"]!.ToObject<decimal>());

        return new DeviceColorXy { X = colorX.Value, Y = colorY.Value };
    }
}
