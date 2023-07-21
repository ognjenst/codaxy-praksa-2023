using MediatR;

namespace SOC.Conductor.Models.Requests
{
    public record ResumeWorkflow(ResumeWorkflowRequestDto resumeDto) : IRequest;
}
