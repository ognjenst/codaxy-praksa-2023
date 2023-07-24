using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record CreateAutomationRequest(AutomationDto entity) : IRequest<AutomationDto> { }

    public class CreateAutomationHandler : IRequestHandler<CreateAutomationRequest, AutomationDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AutomationDto> Handle(CreateAutomationRequest request, CancellationToken cancellationToken)
        {
            var automation = new Automation()
            {
                WorkflowId = request.entity.WorkflowId,
                TriggerId = request.entity.TriggerId,
            };
            
            var result = await _unitOfWork.Automations.CreateAsync(automation, cancellationToken);

            await _unitOfWork.SaveAllAsync();
            return new AutomationDto()
            {
                WorkflowId = result.WorkflowId,
                TriggerId = result.TriggerId
            };
        }
    }
}
