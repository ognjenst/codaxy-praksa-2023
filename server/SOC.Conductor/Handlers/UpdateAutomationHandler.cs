using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record UpdateAutomationRequest(int workflowId, int triggerId, AutomationDto automationDto) : IRequest<AutomationDto> { }

    public class UpdateAutomationHandler : IRequestHandler<UpdateAutomationRequest, AutomationDto>
    {
        private IUnitOfWork _unitOfWork;

        public UpdateAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AutomationDto> Handle(UpdateAutomationRequest request, CancellationToken cancellationToken)
        {
            var automation = (await _unitOfWork.Automations.GetByCondition(x => x.WorkflowId == request.workflowId && x.TriggerId == request.triggerId, cancellationToken)).FirstOrDefault();
            if (automation is not null)
            {
                automation.WorkflowId = request.workflowId;
                automation.TriggerId = request.triggerId;
                automation.Name = request.automationDto.Name;
                automation.InputParameters = request.automationDto.InputParameters;

                var result = await _unitOfWork.Automations.UpdateAsync(automation, cancellationToken);
                await _unitOfWork.SaveAllAsync();

                return new AutomationDto()
                {
                    WorkflowId = result.WorkflowId,
                    TriggerId = result.TriggerId,
                    Name = result.Name,
                    InputParameters = result.InputParameters
                };
            }
            else
                return null;
        }
    }
}
