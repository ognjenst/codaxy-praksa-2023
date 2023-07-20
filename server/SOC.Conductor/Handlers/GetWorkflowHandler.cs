using MediatR;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public class GetWorkflowHandler  : IRequest<IEnumerable<Workflow>>
    {
    }
}
