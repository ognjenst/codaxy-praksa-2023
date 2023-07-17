using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOC.IoT.Base.Infrastructure.Zigbee.Properties;
using SOC.IoT.Domain.Enum;

namespace SOC.IoT.Base.Infrastructure.Zigbee;

internal class DeviceDescription
{
    private MqttDevice _device { get; set; }

    private Dictionary<string, BinaryValue> _binaryValues = new();
    private Dictionary<string, NumericValue> _numericValues = new();
    private Dictionary<string, CompositeValue> _compositeValues = new();
    private Dictionary<string, EnumValue> _enumValues = new();
    private HashSet<DeviceCapability> _capabilities = new();

    public Dictionary<string, BinaryValue> BinaryValues => _binaryValues;
    public Dictionary<string, NumericValue> NumericValues => _numericValues;
    public Dictionary<string, CompositeValue> CompositeValues => _compositeValues;
    public Dictionary<string, EnumValue> EnumValues => _enumValues;

    public string PhysicalAddress { get; private set; }
    public string Id { get; private set; }
    public HashSet<DeviceCapability> Capabilities => _capabilities;

    private static Dictionary<string, DeviceCapability> _deviceCapabilityDict =
        new()
        {
            ["brightness"] = DeviceCapability.Light,
            ["temperature"] = DeviceCapability.Temperature,
            ["humidity"] = DeviceCapability.Humidity,
            ["battery"] = DeviceCapability.Battery,
            ["contact"] = DeviceCapability.Contact,
            ["color_xy"] = DeviceCapability.ColorXy,
            ["power"] = DeviceCapability.Energy,
            ["energy"] = DeviceCapability.Energy,
            ["voltage"] = DeviceCapability.Energy,
            ["current"] = DeviceCapability.Energy,
            ["action"] = DeviceCapability.Switch,
            ["state"] = DeviceCapability.State
        };

    private DeviceCapability? DetermineCapability(Expose expose)
    {
        DeviceCapability result;

        var deviceProperty = expose.Name;
        if (deviceProperty is null)
            return null;

        var success = _deviceCapabilityDict.TryGetValue(deviceProperty, out result);
        return success ? result : null;
    }

    public DeviceDescription(JObject exposes)
    {
        if (exposes is null)
        {
            throw new InvalidOperationException("Device properties are null.");
        }

        if (exposes["ieee_address"] is null && exposes["address"] is null)
        {
            throw new InvalidOperationException("Address is not present in device properties.");
        }

        _device = exposes.ToObject<MqttDevice>()!;
        PhysicalAddress = $"zigbee2mqtt/{_device.IeeeAddress}";
        Id = _device.IeeeAddress;

        if (_device.Definition is null)
        {
            return;
        }

        List<Expose> deviceFeatures = new();
        if (_device.Definition.Exposes.Any(e => e.Features?.Count() > 0))
        {
            deviceFeatures.AddRange(
                _device.Definition.Exposes.First(e => e.Features?.Count() > 0).Features
            );

            var nonExposed = _device.Definition.Exposes.Where(e => e.Features is null);

            if (nonExposed.Count() > 0)
            {
                deviceFeatures.AddRange(nonExposed);
            }
        }
        else
        {
            deviceFeatures.AddRange(_device.Definition.Exposes);
        }

        foreach (var feature in deviceFeatures)
        {
            if (feature is null || feature.Name is null)
            {
                continue;
            }

            var capability = DetermineCapability(feature);
            if (capability is not null)
            {
                _capabilities.Add(capability.Value);
            }

            switch (feature.Type)
            {
                case "binary":
                    _binaryValues[feature.Name] = new BinaryValue(
                        JsonConvert.SerializeObject(feature)
                    );
                    break;
                case "numeric":
                    _numericValues[feature.Name] = new NumericValue(feature);
                    break;
                case "composite":
                    _compositeValues[feature.Name] = new CompositeValue(feature);
                    break;
                case "enum":
                    _enumValues[feature.Name] = new EnumValue(feature);
                    break;
                default:
                    break;
            }
        }
    }
}
