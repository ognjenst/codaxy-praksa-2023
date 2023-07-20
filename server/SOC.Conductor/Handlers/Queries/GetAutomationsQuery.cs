using MediatR;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers.Queries
{
    public record GetAutomationsQuery() : IRequest<IEnumerable<Automation>>;
}
