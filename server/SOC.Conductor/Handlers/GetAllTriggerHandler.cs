using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record GetAllTriggersRequest(string type) : IRequest<IEnumerable<CommonTriggerDto>> { }

    public class GetAllTriggerHandler : IRequestHandler<GetAllTriggersRequest, IEnumerable<CommonTriggerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTriggerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CommonTriggerDto>> Handle(GetAllTriggersRequest request, CancellationToken cancellationToken)
        {
            if(request.type == "PeriodicTrigger")
            {
                var res =  await _unitOfWork.PeriodicTriggers.GetAllAsync(cancellationToken);
                var dtos = res.Select(trigger => new CommonTriggerDto
                {
                    Id = trigger.Id,    
                    Name = trigger.Name,
                });
                return dtos;
            }
            if (request.type == "IoTTrigger")
            {
                var res = await _unitOfWork.IoTTriggers.GetAllAsync(cancellationToken);
                var dtos = res.Select(trigger => new CommonTriggerDto
                {
                    Property = trigger.Property,
                    Value = trigger.Value,
                    Condition = trigger.Condition,
                });
                return dtos;
            }
            return null;
        }
    }
}
