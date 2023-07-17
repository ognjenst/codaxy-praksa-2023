namespace SOC.IoT.Domain.Entity;

public class DeviceHumidity
{
    public decimal Value { get; set; }
    public string Unit { get; set; } = "%";
}
