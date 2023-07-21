using MediatR;

namespace SOC.Conductor.Models.Requests
{
    public record PauseWorkflow(PauseWorkflowRequestDto pauseDto) : IRequest;
}
