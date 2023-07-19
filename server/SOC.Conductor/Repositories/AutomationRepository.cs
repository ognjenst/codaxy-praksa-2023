using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class AutomationRepository : RepositoryBase<Automation>, IAutomationRepository
    {
        protected AutomationRepository(SOCDbContext _SOCDbContext) : base(_SOCDbContext) { }
    }
}
