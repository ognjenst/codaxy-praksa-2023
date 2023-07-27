using Microsoft.EntityFrameworkCore;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class AutomationRepository : RepositoryBase<Automation>, IAutomationRepository
    {
        private readonly SOCDbContext _dbContext;
        public AutomationRepository(SOCDbContext _SOCDbContext) : base(_SOCDbContext) 
        {
            _dbContext = _SOCDbContext;
        }

        public async Task<List<Workflow>?> GetWorkflowsByTriggerIdAsync(int triggerId)
        {
            return (await _dbContext.Triggers.Include(t => t.Workflows).Where(t => t.Id == triggerId).FirstOrDefaultAsync())?.Workflows.ToList();
        }
    }
}
