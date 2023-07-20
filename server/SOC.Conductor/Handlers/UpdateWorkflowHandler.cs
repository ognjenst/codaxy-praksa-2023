using MediatR;
using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record UpdateWorkflowRequest(Workflow entity) : IRequest<Workflow> { }

    public class UpdateWorkflowHandler : IRequestHandler<UpdateWorkflowRequest, Workflow>
    {
        private IUnitOfWork _unitOfWork;

        public UpdateWorkflowHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;          
        }

        public async Task<Workflow> Handle(UpdateWorkflowRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Workflows.UpdateAsync(request.entity, cancellationToken);
        }
    }
}
