using System.ComponentModel.DataAnnotations;

namespace SOC.IoT.Domain.Entity;

public class DeviceLight
{
    [Range(0.0, 1.0)]
    public decimal? Brightness { get; set; }
}
