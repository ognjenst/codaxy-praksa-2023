using MediatR;
using SOC.Conductor.Contracts;

namespace SOC.Conductor.Handlers
{
    public record DeleteAutomationRequest(int WorkflowId, int TriggerId) : IRequest { }

    public class DeleteAutomationHandler : IRequestHandler<DeleteAutomationRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAutomationHandler(IUnitOfWork _unitOfWork)
        {
             _unitOfWork = _unitOfWork;
        }

        public async Task Handle(DeleteAutomationRequest request, CancellationToken cancellationToken)
        {
            var automation = (await _unitOfWork.Automations.GetByCondition(x => x.WorkflowId == request.WorkflowId && x.TriggerId == request.TriggerId, cancellationToken)).FirstOrDefault();             
            if (automation is not null)
            {
                await _unitOfWork.Automations.DeleteAsync(automation, cancellationToken);
                await _unitOfWork.SaveAllAsync();
            }
        }
    }
}
