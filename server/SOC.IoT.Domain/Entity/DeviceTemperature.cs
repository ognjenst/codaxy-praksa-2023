namespace SOC.IoT.Domain.Entity;

public class DeviceTemperature
{
    /// <summary>
    /// Device temperature in unit
    /// </summary>
    public decimal Value { get; set; }
    public string Unit { get; set; } = "C";
}
