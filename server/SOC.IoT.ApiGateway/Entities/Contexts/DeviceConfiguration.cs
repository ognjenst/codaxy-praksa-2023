using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SOC.IoT.ApiGateway.Entities.Contexts
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(new Device
            {
                Id = 1,
                IoTId = "0x00178801062f573d",
                Name = "Philips Lightbulb",
                Description = "N/A",
                Manufacturer = "Philips",
                Model = "9290022166",
                Type = Enums.DeviceType.LIGHTBULB
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
                Type = Enums.DeviceType.SENSOR
            }, new Device
            {
                Id = 7,
                IoTId = "0x00158d0001dd7e46",
                Name = "Light bulb",
                Description = "Smart 7W E27 light bulb",
                Manufacturer = "Nue / 3A",
                Model = "HGZB-06A",
                Type = Enums.DeviceType.LIGHTBULB
            }
            );

        }
    }
}
