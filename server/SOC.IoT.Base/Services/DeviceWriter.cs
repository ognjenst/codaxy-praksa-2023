using Newtonsoft.Json.Linq;
using SOC.IoT.Base.Infrastructure;
using SOC.IoT.Base.Infrastructure.Zigbee;
using SOC.IoT.Base.Infrastructure.Zigbee.Parsers;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;
using SOC.IoT.Domain.Enum;

namespace SOC.IoT.Base.Services;

internal class DeviceWriter : IDeviceWriter
{
    private readonly IDeviceDescriptionManager _descriptionManager;

    public DeviceWriter(IDeviceDescriptionManager descriptionManager)
    {
        _descriptionManager = descriptionManager;
    }

    JObject IDeviceWriter.GeneratePayload(Device device)
    {
        var description = _descriptionManager.GetPayload(device.Id);

        if (description is null)
        {
            throw new InvalidOperationException(
                $"Description for device {device.Id} has not been registered."
            );
        }

        JObject result = new();

        foreach (var capability in device.Capabilities)
        {
            switch (capability)
            {
                case DeviceCapability.State:
                    if (
                        device.State is not null
                        && device.State.State is not null
                        && description.BinaryValues.ContainsKey("state")
                    )
                        result["state"] = JToken.FromObject(
                            description.BinaryValues["state"].SetValue(device.State.State)
                        );
                    break;
                case DeviceCapability.Light:
                    if (
                        device.Light is not null
                        && device.Light.Brightness is not null
                        && description.NumericValues.ContainsKey("brightness")
                    )
                        result["brightness"] = description.NumericValues[
                            "brightness"
                        ].SetPercentage(device.Light.Brightness.Value);
                    break;
                case DeviceCapability.ColorXy:
                    if (
                        device.ColorXy is not null
                        && device.ColorXy is not null
                        && description.CompositeValues.ContainsKey("color_xy")
                    )
                    {
                        result["color"] = JObject.FromObject(new object());
                        result["color"]!["x"] = description.CompositeValues[
                            "color_xy"
                        ].NumericValues["x"].SetValue(device.ColorXy.X);

                        result["color"]!["y"] = description.CompositeValues[
                            "color_xy"
                        ].NumericValues["y"].SetValue(device.ColorXy.Y);
                    }
                    break;
                default:
                    break;
            }
        }
        return result;
    }

    Device IDeviceWriter.ReadDevice(DeviceDescription description, JObject payload)
    {
        if (description is null)
            throw new InvalidOperationException($"No description provided.");

        var device = new Device { Id = description.Id, Capabilities = description.Capabilities, };

        foreach (var capability in description.Capabilities)
        {
            switch (capability)
            {
                case DeviceCapability.State:
                    device.State = description.ParseState(payload);
                    break;
                case DeviceCapability.Light:
                    device.Light = description.ParseLight(payload);
                    break;
                case DeviceCapability.ColorXy:
                    device.ColorXy = description.ParseColorXy(payload);
                    break;
                case DeviceCapability.Temperature:
                    device.Temperature = description.ParseTemperature(payload);
                    break;
                case DeviceCapability.Humidity:
                    device.Humidity = description.ParseHumidity(payload);
                    break;
                case DeviceCapability.Energy:
                    device.Energy = description.ParseEnergy(payload);
                    break;
                default:
                    break;
            }
        }

        return device;
    }
}
