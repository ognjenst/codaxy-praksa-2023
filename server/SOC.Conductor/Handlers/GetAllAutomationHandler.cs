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
                    var triggerDto = new CommonTriggerDto()
                    {
                        Id = automation.Trigger.Id,
                        Name = automation.Trigger.Name
                    };
                    if (automation.Trigger is PeriodicTrigger)
                    {
                        var periodicTrigger = automation.Trigger as PeriodicTrigger;
                        triggerDto.Start = periodicTrigger.Start;
                        triggerDto.Period = periodicTrigger.Period;
                        triggerDto.Unit = periodicTrigger.Unit;
                    } else if (automation.Trigger is IoTTrigger) 
                    {
                        var iotTrigger = automation.Trigger as IoTTrigger;
                        triggerDto.Property = iotTrigger.Property;
                        triggerDto.Value = iotTrigger.Value;
                        triggerDto.Condition = iotTrigger.Condition;
                    }
                    return new AutomationDto
                    {
                        Trigger = triggerDto,
                        Workflow = new WorkflowDto
                        {
                            Id = automation.Workflow.Id,
                            Name = automation.Workflow.Name,
                            CreateDate = automation.Workflow.CreatedAt,
                            Version = automation.Workflow.Version,
                            Enabled = automation.Workflow.Enabled,
                            UpdateDate = automation.Workflow.UpdatedAt,
                        },
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
