using SOC.Conductor.Contracts;
using SOC.Conductor.Entities.Contexts;

namespace SOC.Conductor.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SOCDbContext _dbContext;

        private IAutomationRepository _automationRepository;
        private IIoTTriggerRepository _ioTTriggerRepository;
        private IPeriodicTriggerRepository _periodicTriggerRepository;
        private ITriggerRepository _triggerRepository;
        private IWorkflowRepository _workflowRepository;

        public IAutomationRepository Automations
        {
            get => _automationRepository ??= new AutomationRepository(_dbContext);
        }
        public IIoTTriggerRepository IoTTriggers
        {
            get => _ioTTriggerRepository ??= new IoTTriggerRepository(_dbContext);
        }
        public IPeriodicTriggerRepository PeriodicTriggers
        {
            get => _periodicTriggerRepository ??= new PeriodicTriggerRepository(_dbContext);
        }
        public ITriggerRepository Triggers
        {
            get => _triggerRepository ??= new TriggerRepository(_dbContext);
        }
        public IWorkflowRepository Workflows
        {
            get => _workflowRepository ??= new WorkflowRepository(_dbContext);
        }

        public UnitOfWork(SOCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveAllAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool dispose)
        {
            _dbContext.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
