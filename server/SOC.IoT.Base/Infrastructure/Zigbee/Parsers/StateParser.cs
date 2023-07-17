using Newtonsoft.Json.Linq;
using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base.Infrastructure.Zigbee.Parsers;

internal static class StateParser
{
    public static DeviceState? ParseState(this DeviceDescription description, JObject payload)
    {
        var state = payload["state"];

        if (payload is null || state is null || !description.BinaryValues.ContainsKey("state"))
            return null;

        var stateValue = description.BinaryValues["state"];
        stateValue.SetValue(state.ToObject<object>()!);

        return new DeviceState { State = stateValue.Value };
    }
}
