using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SOC.IoT.ApiGateway.Entities.Enums;

namespace SOC.IoT.ApiGateway.Entities.Contexts
{
    public class SOCIoTDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHistory> DevicesHistory { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
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

            /*modelBuilder.ApplyConfiguration(new PermissionConfiguration());*/
            

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName).HasMaxLength(45).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(45).IsRequired();
                entity.Property(e => e.Username).HasMaxLength(45).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(45).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Salt);
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).HasMaxLength(45);
                entity.HasMany(e => e.Users)
                    .WithOne(d => d.Role)
                    .HasForeignKey(
                        l => l.RoleId
                    );
                entity.HasMany(e => e.Permissions)
                      .WithOne(l => l.Role)
                      .HasForeignKey(l => l.RoleId);

                entity.HasData(new Role { Id = 1, Name = "Admin" });
                entity.HasData(new Role { Id = 2, Name = "User" });
            });

           

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
