using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record CreateAutomationRequest(Automation entity) : IRequest<Automation> { }

    public class CreateAutomationHandler : IRequestHandler<CreateAutomationRequest, Automation>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Automation> Handle(CreateAutomationRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Automations.CreateAsync(request.entity, cancellationToken);
        }
    }
}
