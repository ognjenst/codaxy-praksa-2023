using Microsoft.EntityFrameworkCore;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class AutomationRepository : RepositoryBase<Automation>, IAutomationRepository
    {
        private readonly SOCDbContext _dbContext;

        public AutomationRepository(SOCDbContext _SOCDbContext)
            : base(_SOCDbContext)
        {
            _dbContext = _SOCDbContext;
        }

        public async Task<Automation> GetById(int triggerId, int workflowId)
        {
            return await _dbContext.Automations.Where((a) => a.WorkflowId == workflowId && a.TriggerId == triggerId).FirstAsync();
        }

        public async Task<List<Workflow>> GetWorkflowsByTriggerIdAsync(int triggerId)
        {
            var workflowIds = await _dbContext.Automations
                .Where(a => a.TriggerId == triggerId)
                .Select(e => e.WorkflowId)
                .ToListAsync();
            return await _dbContext.Workflows.Where(e => workflowIds.Contains(e.Id)).ToListAsync();
        }
    }
}
