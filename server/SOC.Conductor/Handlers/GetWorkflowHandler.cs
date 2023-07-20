using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record GetAllWorkflowsRequest() : IRequest<IEnumerable<Workflow>> { }

    public class GetAllWorkflowHandler : IRequestHandler<GetAllWorkflowsRequest, IEnumerable<Workflow>>
    {
        private IUnitOfWork _unitOfWork;

        public GetAllWorkflowHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Workflow>> Handle(GetAllWorkflowsRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Workflows.GetAllAsync(cancellationToken);
        }
    }
}
