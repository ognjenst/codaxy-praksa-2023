using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;
using System.Linq.Expressions;

namespace SOC.Conductor.Repositories
{
    public class IoTTriggerRepository : RepositoryBase<IoTTrigger>, IIoTTriggerRepository
    {
        public IoTTriggerRepository(SOCDbContext dbContext) : base(dbContext) { }

        public async Task<IList<Workflow>> GetWorkflowsByTriggerIdAsync(int triggerId)
        {
            return await _dbContext.Triggers.Where(t => t.Id == triggerId).SelectMany(t => t.Workflows).ToListAsync();
        }
    }
}
