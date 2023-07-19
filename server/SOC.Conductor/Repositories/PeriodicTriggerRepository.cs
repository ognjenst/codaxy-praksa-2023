using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class PeriodicTriggerRepository : RepositoryBase<PeriodicTrigger>, IPeriodicTriggerRepository
    {
        protected PeriodicTriggerRepository(SOCDbContext _SOCDbContext) : base(_SOCDbContext) { }
    }
}
