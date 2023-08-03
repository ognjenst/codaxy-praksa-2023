using AutoMapper;
using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Generated;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record GetAllAutomationsRequest() : IRequest<IEnumerable<AutomationDto>> { }

    public class GetAllAutomationHandler
        : IRequestHandler<GetAllAutomationsRequest, IEnumerable<AutomationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAutomationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutomationDto>> Handle(
            GetAllAutomationsRequest request,
            CancellationToken cancellationToken
        )
        {
            var res = await _unitOfWork.Automations.GetAllAsync(cancellationToken);
            var dtos = res.Select(
                automation =>
                {
                    var triggerDto = new CommonTriggerDto();
                    if (automation.Trigger is PeriodicTrigger)
                    {
                        var periodicTrigger = automation.Trigger as PeriodicTrigger;
                        triggerDto = _mapper.Map<CommonTriggerDto>(periodicTrigger);
                    } else if (automation.Trigger is IoTTrigger) 
                    {
                        var iotTrigger = automation.Trigger as IoTTrigger;
                        triggerDto = _mapper.Map<CommonTriggerDto>(iotTrigger);
                    }
                    return new AutomationDto
                    {
                        Id = automation.Id,
                        Trigger = triggerDto,
                        Workflow = _mapper.Map<WorkflowDto>(automation.Workflow),
                        TriggerId = automation.TriggerId,
                        WorkflowId = automation.WorkflowId,
                        Name = automation.Name,
                        InputParameters = automation.InputParameters?.ToString()
                    };
                }
                    
            );
            return dtos;
        }
    }
}
