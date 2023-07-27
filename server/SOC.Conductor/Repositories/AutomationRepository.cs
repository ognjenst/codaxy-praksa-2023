using Microsoft.EntityFrameworkCore;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class AutomationRepository : RepositoryBase<Automation>, IAutomationRepository
    {

        public AutomationRepository(SOCDbContext _SOCDbContext)
            : base(_SOCDbContext)
        {
        }
    }
}
