using MediatR;
using Newtonsoft.Json.Linq;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record UpdateAutomationRequest(
        int id,
        AutomationDto automationDto
    ) : IRequest<AutomationDto> { }

    public class UpdateAutomationHandler : IRequestHandler<UpdateAutomationRequest, AutomationDto>
    {
        private IUnitOfWork _unitOfWork;

        public UpdateAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AutomationDto> Handle(
            UpdateAutomationRequest request,
            CancellationToken cancellationToken
        )
        {
            var automation = (
                await _unitOfWork.Automations.GetByCondition(
                    x => x.WorkflowId == request.id,
                    cancellationToken
                )
            ).FirstOrDefault();
            if (automation is not null)
            {
                automation.Name = request.automationDto.Name;
                if (!string.IsNullOrEmpty(request.automationDto.InputParameters))
                {
                    automation.InputParameters = JObject.Parse(
                        request.automationDto.InputParameters!
                    );
                }

                var result = await _unitOfWork.Automations.UpdateAsync(
                    automation,
                    cancellationToken
                );

                await _unitOfWork.SaveAllAsync();

                return new AutomationDto()
                {
                    Id = result.Id,
                    WorkflowId = automation.WorkflowId,
                    TriggerId = automation.TriggerId,
                    Name = result.Name,
                    InputParameters = result.InputParameters?.ToString(),
                };
            }
            else
                return null;
        }
    }
}
