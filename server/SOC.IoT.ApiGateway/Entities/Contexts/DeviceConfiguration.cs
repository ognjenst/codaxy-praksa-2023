using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SOC.IoT.ApiGateway.Entities.Contexts
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.HasMany(x => x.DevicesHistory).WithOne(h => h.Device).HasForeignKey(h => h.DeviceID);

            builder.HasData(new Device
            {
                Id = 1,
                IoTId = "0x00178801062f573d",
                Name = "Philips Lightbulb",
                Description = "Hue white and color ambiance E26/E27",
                Manufacturer = "Philips",
                Model = "9290022166",
                Type = Enums.DeviceType.LIGHTBULB
            }, new Device
            {
                Id = 2,
                IoTId = "0x00124b002268b3b2",
                Name = "SONOFF Wireless button",
                Description = "Wireless button",
                Manufacturer = "SONOFF",
                Model = "SNZB-01",
                Type = Enums.DeviceType.BUTTON
            }, new Device
            {
                Id = 3,
                IoTId = "0x00124b0022d2d320",
                Name = "SONOFF Temp and Humidity Sensor",
                Description = "Temperature and humidity sensor",
                Manufacturer = "SONOFF",
                Model = "SNZB-02",
                Type = Enums.DeviceType.SENSOR
            }, new Device
            {
                Id = 4,
                IoTId = "0x04cf8cdf3c7597cd",
                Name = "Xiaomi Mi Socket",
                Description = "Mi power plug ZigBee EU",
                Manufacturer = "Xiaomi",
                Model = "ZNCZ04LM",
                Type = Enums.DeviceType.SOCKET
            }, new Device
            {
                Id = 5,
                IoTId = "0x00124b00226969ac",
                Name = "Contact Sensor",
                Description = "Contact sensor",
                Manufacturer = "SONOFF",
                Model = "SNZB-04",
                Type = Enums.DeviceType.SENSOR
            }, new Device
            {
                Id = 6,
                IoTId = "0xa4c13893cdd87674",
                Name = "Wireless switch",
                Description = "Wireless switch with 4 buttons",
                Manufacturer = "TuYa",
                Model = "TS0044",
                Type = Enums.DeviceType.BUTTON
            }, new Device
            {
                Id = 7,
                IoTId = "0x00158d0001dd7e46",
                Name = "Light bulb",
                Description = "Smart 7W E27 light bulb",
                Manufacturer = "Nue / 3A",
                Model = "HGZB-06A",
                Type = Enums.DeviceType.LIGHTBULB
            });
        }
    }
}
