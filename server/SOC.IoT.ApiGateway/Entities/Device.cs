using SOC.IoT.ApiGateway.Entities.Enums;

namespace SOC.IoT.ApiGateway.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string IoTId { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public DeviceType Type { get; set; }
        public string Model { get; set; }
        public virtual ICollection<DeviceHistory> DevicesHistory { get; set; }
    }
}
