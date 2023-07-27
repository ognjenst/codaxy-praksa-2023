using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOC.IoT.ApiGateway.Entities.Enums;

namespace SOC.IoT.ApiGateway.Entities.Contexts
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired();


            // Add permissions for Admin
            builder.HasData(new Permission { Id = 1, Name = "Create-Workflow", RoleId = 1 },
            new Permission { Id = 2, Name = "Update-Workflow", RoleId = 1 },
            new Permission { Id = 3, Name = "Read-Workflow", RoleId = 1 },
            new Permission { Id = 4, Name = "Delete-Workflow", RoleId = 1 },
            new Permission { Id = 5, Name = "Create-Trigrer", RoleId = 1 },
            new Permission { Id = 6, Name = "Update-Trigger", RoleId = 1 },
            new Permission { Id = 7, Name = "Read-Trigger", RoleId = 1 },
            new Permission { Id = 8, Name = "Delete-Trigger", RoleId = 1 },
            new Permission { Id = 9, Name = "Create-Automation", RoleId = 1 },
            new Permission { Id = 10, Name = "Update-Automation", RoleId = 1 },
            new Permission { Id = 11, Name = "Read-Automation", RoleId = 1 },
            new Permission { Id = 12, Name = "Delete-Automation", RoleId = 1 },
            new Permission { Id = 13, Name = "Create-Device", RoleId = 1 },
            new Permission { Id = 14, Name = "Update-Device", RoleId = 1 },
            new Permission { Id = 15, Name = "Read-Device", RoleId = 1 },
            new Permission { Id = 16, Name = "Delete-Device", RoleId = 1 },
            new Permission { Id = 17, Name = "Create-DeviceHistory", RoleId = 1 },
            new Permission { Id = 18, Name = "Update-DeviceHistory", RoleId = 1 },
            new Permission { Id = 19, Name = "Read-DeviceHistory", RoleId = 1 },
            new Permission { Id = 20, Name = "Delete-DeviceHistory", RoleId = 1 });

        }
    }
}