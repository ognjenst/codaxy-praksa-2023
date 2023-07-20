using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record CreateWorkflowRequest(Workflow entity) : IRequest<Workflow> { }

    public class CreateWorkflowHandler : IRequestHandler<CreateWorkflowRequest, Workflow>
    {
        private IUnitOfWork _unitOfWork;

        public CreateWorkflowHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Workflow> Handle(CreateWorkflowRequest request, CancellationToken cancellationToken)
        {
            var result =  await _unitOfWork.Workflows.CreateAsync(request.entity, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return result;
        }
    }
}
