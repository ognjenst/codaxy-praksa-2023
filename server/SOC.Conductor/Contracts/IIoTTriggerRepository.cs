using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SOC.Conductor.Entities;
using System.Linq.Expressions;

namespace SOC.Conductor.Contracts
{
    public interface IIoTTriggerRepository : IRepositoryBase<IoTTrigger>
    {
        Task<IList<Workflow>> GetWorkflowsByTriggerIdAsync(int triggerId);
    }
}
