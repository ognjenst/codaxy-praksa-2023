using AutoMapper;
using MediatR;
using SOC.Conductor.Generated;
using SOC.Conductor.Models;
using SOC.Conductor.Models.Requests;

namespace SOC.Conductor.Handlers
{
    public class PauseWorkflowHandler : IRequestHandler<PauseWorkflow>
    {
        private readonly IWorkflowResourceClient _client;

        public PauseWorkflowHandler(IWorkflowResourceClient client)
        {
            _client = client;
        }

        public async System.Threading.Tasks.Task Handle(PauseWorkflow request, CancellationToken cancellationToken)
        {
            string workflowId = request.pauseDto.WorkflowId;

            await _client.PauseWorkflowAsync(workflowId);
        }
    }
}
