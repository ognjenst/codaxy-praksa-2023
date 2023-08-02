namespace SOC.Conductor.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAutomationRepository Automations { get; }
        IIoTTriggerRepository IoTTriggers { get; }
        IPeriodicTriggerRepository PeriodicTriggers { get; }
        IWorkflowRepository Workflows { get; }
        Task<int> SaveAllAsync(); 
    }
}
