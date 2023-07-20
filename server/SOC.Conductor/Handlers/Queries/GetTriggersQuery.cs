using MediatR;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers.Queries
{
    public record GetTriggersQuery() : IRequest<IEnumerable<Trigger>>;
}
