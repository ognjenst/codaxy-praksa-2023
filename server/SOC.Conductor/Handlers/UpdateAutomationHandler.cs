using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record UpdateAutomationRequest(AutomationDto entity) : IRequest<AutomationDto> { }

    public class UpdateAutomationHandler : IRequestHandler<UpdateAutomationRequest, AutomationDto>
    {
        private IUnitOfWork _unitOfWork;

        public UpdateAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AutomationDto> Handle(UpdateAutomationRequest request, CancellationToken cancellationToken)
        {
            var automation = new Automation()
            {
                WorkflowId = request.entity.WorkflowId,
                TriggerId = request.entity.TriggerId,
            };

            var result = await _unitOfWork.Automations.UpdateAsync(automation, cancellationToken);

            await _unitOfWork.SaveAllAsync();
            return new AutomationDto()
            {
                WorkflowId = result.WorkflowId,
                TriggerId = result.TriggerId
            };
        }
    }
}
