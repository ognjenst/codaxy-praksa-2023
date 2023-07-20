using MediatR;
using SOC.Conductor.Contracts;

namespace SOC.Conductor.Handlers
{
    public record DeleteWorkflowRequest(int WorkflowId) : IRequest { }

    public class DeleteWorkflowHandler : IRequestHandler<DeleteWorkflowRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkflowHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task Handle(DeleteWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = (await _unitOfWork.Workflows.GetByCondition(x => x.Id == request.WorkflowId, cancellationToken)).FirstOrDefault();
            if (workflow is not null)
            {
                await _unitOfWork.Workflows.DeleteAsync(workflow, cancellationToken);
                await _unitOfWork.SaveAllAsync();
            }
        }
    }
}
