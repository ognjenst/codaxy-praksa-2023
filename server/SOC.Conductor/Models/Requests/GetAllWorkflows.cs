using MediatR;

namespace SOC.Conductor.Models.Requests
{
    public record GetAllWorkflows() : IRequest<IEnumerable<WorkflowResponseDto>>;
}
