using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record GetAllAutomationsRequest() : IRequest<IEnumerable<Automation>> { }

    public class GetAllAutomationHandler : IRequestHandler<GetAllAutomationsRequest, IEnumerable<Automation>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAutomationHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Automation>> Handle(GetAllAutomationsRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Automations.GetAllAsync(cancellationToken);
        }
    }
}
