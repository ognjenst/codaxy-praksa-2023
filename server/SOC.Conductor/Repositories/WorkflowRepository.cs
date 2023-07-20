using IoT.Conductor.Services;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class WorkflowRepository : RepositoryBase<Workflow>, IWorkflowRepository
    {
        public WorkflowRepository(SOCDbContext _SOCDbContext) : base(_SOCDbContext) { }
    }
}
