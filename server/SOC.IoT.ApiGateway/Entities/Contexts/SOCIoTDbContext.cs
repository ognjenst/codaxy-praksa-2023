using Microsoft.EntityFrameworkCore;

namespace SOC.IoT.ApiGateway.Entities.Contexts
{
    public class SOCIoTDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHistory> DevicesHistory { get; set; }
        public SOCIoTDbContext(DbContextOptions<SOCIoTDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
