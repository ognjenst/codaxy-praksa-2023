using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record GetAllAutomationsRequest() : IRequest<IEnumerable<AutomationDto>> { }

    public class GetAllAutomationHandler : IRequestHandler<GetAllAutomationsRequest, IEnumerable<AutomationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AutomationDto>> Handle(GetAllAutomationsRequest request, CancellationToken cancellationToken)
        {
            var res =  await _unitOfWork.Automations.GetAllAsync(cancellationToken);
            var dtos = res.Select(automation => new AutomationDto
            {
                TriggerId = automation.TriggerId,
                WorkflowId = automation.WorkflowId,
                Name = automation.Name,
                InputParameters = automation.InputParameters
            });
            return dtos;
        }
    }
}
