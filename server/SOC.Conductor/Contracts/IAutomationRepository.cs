using SOC.Conductor.Entities;

namespace SOC.Conductor.Contracts
{
    public interface IAutomationRepository : IRepositoryBase<Automation>
    {
        public Task<List<Workflow>> GetWorkflowsByTriggerIdAsync(int triggerId);
        public Task<Automation> GetAutomationByWorkflowAndTriggerAsync(int workflowId, int triggerId);
    }
}
