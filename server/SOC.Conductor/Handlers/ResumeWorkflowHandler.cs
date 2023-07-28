using MediatR;
using SOC.Conductor.Generated;
using SOC.Conductor.Models.Requests;

namespace SOC.Conductor.Handlers;

using Task = System.Threading.Tasks.Task;
public class ResumeWorkflowHandler : IRequestHandler<ResumeWorkflow>
{
    private readonly IWorkflowResourceClient _client;

    public ResumeWorkflowHandler(IWorkflowResourceClient client)
    {
        _client = client;
    }

    public async Task Handle(ResumeWorkflow request, CancellationToken cancellationToken)
    {
        string workflowId = request.resumeDto.WorkflowId;

        await _client.ResumeWorkflowAsync(workflowId);
    }
}
