namespace SOC.IoT.Domain.Entity;

public class DeviceEnergy
{
    /// <summary>
    /// Sum of consumed energy in kWh
    /// </summary>
    public decimal? Total { get; set; }

    /// <summary>
    /// Instantaneous measured electrical current in amperes
    /// </summary>
    public decimal? Current { get; set; }

    /// <summary>
    /// Measured electrical potential value in volts
    /// </summary>
    public decimal? Voltage { get; set; }

    /// <summary>
    /// Instantaneous measured power in watts
    /// </summary>
    public decimal? Power { get; set; }
}
