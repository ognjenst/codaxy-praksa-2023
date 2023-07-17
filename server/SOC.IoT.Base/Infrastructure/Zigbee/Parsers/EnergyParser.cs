using Newtonsoft.Json.Linq;
using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Parsers;

internal static class EnergyParser
{
    public static DeviceEnergy? ParseEnergy(this DeviceDescription description, JObject payload)
    {
        var power = payload["power"];
        var energy = payload["energy"];
        var voltage = payload["voltage"];
        var current = payload["current"];

        decimal? powerValue = null,
            energyValue = null,
            voltageValue = null,
            currentValue = null;

        if (payload is null)
            return null;

        if (power is not null && description.NumericValues.ContainsKey("power"))
        {
            var numVal = description.NumericValues["power"];
            numVal.SetValue(power.ToObject<decimal>()!);
            powerValue = numVal.Value;
        }
        if (energy is not null && description.NumericValues.ContainsKey("energy"))
        {
            var numVal = description.NumericValues["energy"];
            numVal.SetValue(energy.ToObject<decimal>()!);
            energyValue = numVal.Value;
        }
        if (voltage is not null && description.NumericValues.ContainsKey("voltage"))
        {
            var numVal = description.NumericValues["voltage"];
            numVal.SetValue(voltage.ToObject<decimal>()!);
            voltageValue = numVal.Value;
        }
        if (current is not null && description.NumericValues.ContainsKey("current"))
        {
            var numVal = description.NumericValues["current"];
            numVal.SetValue(current.ToObject<decimal>()!);
            currentValue = numVal.Value;
        }

        return new DeviceEnergy
        {
            Total = energyValue,
            Power = powerValue,
            Voltage = voltageValue,
            Current = currentValue
        };
    }
}
