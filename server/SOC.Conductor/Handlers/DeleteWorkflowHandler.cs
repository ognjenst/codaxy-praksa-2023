using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Generated;

namespace SOC.Conductor.Handlers
{
    public record DeleteWorkflowRequest(string WorkflowName, int Version = 1) : IRequest { }

    public class DeleteWorkflowHandler : IRequestHandler<DeleteWorkflowRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMetadataResourceClient _client;

        public DeleteWorkflowHandler(IUnitOfWork unitOfWork, IMetadataResourceClient client)
        {
            _unitOfWork = unitOfWork;
            _client = client;
        }

        public async System.Threading.Tasks.Task Handle(DeleteWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = (await _unitOfWork.Workflows.GetByCondition(x => x.Name == request.WorkflowName && x.Version == request.Version, cancellationToken)).FirstOrDefault();
            await _client.UnregisterWorkflowDefAsync(request.WorkflowName, request.Version, cancellationToken);

            if (workflow is not null)
            {
                var result = await _unitOfWork.Workflows.DeleteAsync(workflow, cancellationToken);
                await _unitOfWork.SaveAllAsync();
            }
        }
    }
}
