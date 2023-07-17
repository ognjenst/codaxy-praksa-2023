using SOC.IoT.Domain.Entity;
using SOC.IoT.Domain.Enum;

namespace SOC.IoT.ApiGateway.Models;

/// <summary>
/// Class used to update a device's state
/// </summary>
public class DeviceUpdateDTO
{
    /// <summary>
    /// State of the device (e.g. true = on, false = off)
    /// </summary>
    public DeviceState? State { get; set; }

    /// <summary>
    /// Brightness of the light
    /// </summary>
    public DeviceLight? Light { get; set; }

    /// <summary>
    /// x and y coordinates in xyy' colorspace
    /// </summary>
    public DeviceColorXy? ColorXy { get; set; }

    /// <summary>
    /// Constructs a <see cref="Device">Device</see> object with the provided <paramref name="deviceId"/>
    /// </summary>
    /// <param name="deviceId">ID of the device to be updates</param>
    /// <returns> <see cref="Device">Device</see> object with computed capabilities</returns>
    public Device GetDevice(string deviceId)
    {
        var device = new Device
        {
            Id = deviceId,
            Light = Light,
            State = State,
            ColorXy = ColorXy,
            Capabilities = new HashSet<DeviceCapability>()
        };

        if (State is not null)
            device.Capabilities.Add(DeviceCapability.State);
        if (Light is not null)
            device.Capabilities.Add(DeviceCapability.Light);
        if (ColorXy is not null)
            device.Capabilities.Add(DeviceCapability.ColorXy);

        return device;
    }
}
