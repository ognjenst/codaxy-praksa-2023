using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class TriggerRepository : RepositoryBase<Trigger>, ITriggerRepository
    {
        public TriggerRepository(SOCDbContext _SOCDbContext)
            : base(_SOCDbContext) { }
    }
}
