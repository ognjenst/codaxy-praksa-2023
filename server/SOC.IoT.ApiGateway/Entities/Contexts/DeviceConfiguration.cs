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
            });
        }
    }
}
