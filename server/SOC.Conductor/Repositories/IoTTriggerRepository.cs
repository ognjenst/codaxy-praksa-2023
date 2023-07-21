using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class IoTTriggerRepository : RepositoryBase<IoTTrigger>, IIoTTriggerRepository
    {
        public IoTTriggerRepository(SOCDbContext dbContext) : base(dbContext) { }
    }
}
