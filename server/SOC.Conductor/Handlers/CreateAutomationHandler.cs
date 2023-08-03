using MediatR;
using Newtonsoft.Json.Linq;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record CreateAutomationRequest(AutomationDto automationDto) : IRequest<AutomationDto> { }

    public class CreateAutomationHandler : IRequestHandler<CreateAutomationRequest, AutomationDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AutomationDto> Handle(
            CreateAutomationRequest request,
            CancellationToken cancellationToken
        )
        {
            var automation = new Automation()
            {
                WorkflowId = request.automationDto.WorkflowId,
                TriggerId = request.automationDto.TriggerId,
                Name = request.automationDto.Name,
                InputParameters = JObject.Parse(request.automationDto.InputParameters!),
            };

            var result = await _unitOfWork.Automations.CreateAsync(automation, cancellationToken);
            await _unitOfWork.SaveAllAsync();

            return new AutomationDto()
            {
                Id = result.Id,
                WorkflowId = result.WorkflowId,
                TriggerId = result.TriggerId,
                Name = result.Name,
                InputParameters = result.InputParameters?.ToString(),
            };
        }
    }
}
