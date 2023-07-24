using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record DeleteWorkflowRequest(int WorkflowId) : IRequest { }

    public class DeleteWorkflowHandler : IRequestHandler<DeleteWorkflowRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWorkflowHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = (await _unitOfWork.Workflows.GetByCondition(x => x.Id == request.WorkflowId, cancellationToken)).FirstOrDefault();
            if (workflow is not null)
            {
                var result = await _unitOfWork.Workflows.DeleteAsync(workflow, cancellationToken);
                await _unitOfWork.SaveAllAsync();
            }
        }
    }
}
