using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record UpdateAutomationRequest(Automation entity) : IRequest<Automation> { }

    public class UpdateAutomationHandler : IRequestHandler<UpdateAutomationRequest, Automation>
    {
        private IUnitOfWork _unitOfWork;

        public UpdateAutomationHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public Task<Automation> Handle(UpdateAutomationRequest request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Automations.UpdateAsync(request.entity, cancellationToken);
        }
    }
}
