using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SOC.IoT.ApiGateway.Entities.Contexts
{
    public class SOCIoTDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHistory> DevicesHistory { get; set; }
        public SOCIoTDbContext(DbContextOptions<SOCIoTDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceHistory>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Configuration).HasConversion(
                   v => JsonConvert.SerializeObject(v),
                   v => JsonConvert.DeserializeObject<JObject>(v)).HasColumnType("jsonb");
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
