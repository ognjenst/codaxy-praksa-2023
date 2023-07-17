using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SOC.IoT.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace SOC.IoT.ApiGateway.Models;

/// <summary>
/// Provides a device with its ID and capabilities
/// </summary>
public class DeviceDTO
{
    /// <summary>
    /// Id of the device
    /// </summary>
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// List of the device's capabilities
    /// </summary>
    [Required]
    public IEnumerable<string> Capabilities { get; set; }

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
    /// Sensor temperature with its unit (default C)
    /// </summary>
    public DeviceTemperature? Temperature { get; set; }

    /// <summary>
    /// Sensor humidity with its unit (default %)
    /// </summary>
    public DeviceHumidity? Humidity { get; set; }

    /// <summary>
    /// Energy consumption and live power
    /// </summary>
    public DeviceEnergy? Energy { get; set; }

    /// <summary>
    /// State of the contact sensor
    /// </summary>
    public DeviceContact? Contact { get; set; }

    public DeviceDTO(Device device)
    {
        var converter = new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() };

        Id = device.Id;
        Capabilities = device.Capabilities.Select(c =>
        {
            var name = c.ToString();

            return $"{name[0]}".ToLower() + name.Substring(1, name.Length - 1);
        });
        State = device.State;
        Light = device.Light;
        ColorXy = device.ColorXy;
        Temperature = device.Temperature;
        Humidity = device.Humidity;
        Energy = device.Energy;
        Contact = device.Contact;
    }
}
