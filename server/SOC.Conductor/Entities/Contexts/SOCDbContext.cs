using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOC.Conductor.Entities.Enums;

namespace SOC.Conductor.Entities.Contexts
{
    public class SOCDbContext : DbContext
    {
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<Automation> Automations { get; set; }
        public DbSet<PeriodicTrigger> PeriodicTriggers { get; set; }
        public DbSet<IoTTrigger> IoTTriggers { get; set; }

        public SOCDbContext(DbContextOptions<SOCDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.HasKey(entity => entity.Id);
            });

            modelBuilder.Entity<Trigger>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity
                    .HasMany(e => e.Workflows)
                    .WithMany(e => e.Triggers)
                    .UsingEntity<Automation>(
                        l => l.HasOne<Workflow>().WithMany().HasForeignKey(e => e.WorkflowId),
                        r => r.HasOne<Trigger>().WithMany().HasForeignKey(e => e.TriggerId)
                    );
            });

            modelBuilder.Entity<Trigger>().UseTptMappingStrategy();

            modelBuilder.Entity<Automation>(entity =>

            {
                entity.HasKey(e => new { e.WorkflowId, e.TriggerId });
                entity
                    .Property(e => e.InputParameters)
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<JObject?>(v)
                    )
                    .HasColumnType("jsonb");    
            });

            modelBuilder.Entity<IoTTrigger>(entity =>
            {
                entity.Property(p => p.Condition).HasConversion(v => v.ToString(), v => (Operator)Enum.Parse(typeof(Operator), v));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
