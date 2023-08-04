using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Models;

namespace SOC.Conductor.Handlers
{
    public record CreateTriggerRequest(string Type, CommonTriggerDto commonTriggerDto)
        : IRequest<CommonTriggerDto> { }

    public class CreateTriggerHandler : IRequestHandler<CreateTriggerRequest, CommonTriggerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreateTriggerHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<CommonTriggerDto> Handle(
            CreateTriggerRequest request,
            CancellationToken cancellationToken
        )
        {
            if (request.Type == nameof(PeriodicTrigger))
            {
                var periodic = new PeriodicTrigger()
                {
                    Name = request.commonTriggerDto.Name,
                    Period = request.commonTriggerDto.Period.GetValueOrDefault(),
                    Start = request.commonTriggerDto.Start.Value.UtcDateTime,
                    Unit = request.commonTriggerDto.Unit.GetValueOrDefault(),
                };

                var result = await _unitOfWork.PeriodicTriggers.CreateAsync(periodic);
                await _unitOfWork.SaveAllAsync();

                await _mediator.Publish(
                    new TriggerNotification
                    {
                        Action = Models.Enums.TriggerNotificationAction.CREATE,
                        Trigger = result
                    }
                );

                return new CommonTriggerDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Period = result.Period,
                    Start = result.Start,
                    Unit = result.Unit
                };
            }

            if (request.Type == nameof(IoTTrigger))
            {
                var iot = new IoTTrigger()
                {
                    Id = request.commonTriggerDto.Id,
                    Name = request.commonTriggerDto.Name,
                    Property = request.commonTriggerDto.Property,
                    Value = request.commonTriggerDto.Value,
                    Condition = request.commonTriggerDto.Condition.GetValueOrDefault(),
                    DeviceId = request.commonTriggerDto.DeviceId
                };

                var result = await _unitOfWork.IoTTriggers.CreateAsync(iot);
                await _unitOfWork.SaveAllAsync();

                await _mediator.Publish(
                    new TriggerNotification
                    {
                        Action = Models.Enums.TriggerNotificationAction.CREATE,
                        Trigger = result
                    }
                );

                return new CommonTriggerDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Property = iot.Property,
                    Value = iot.Value,
                    Condition = iot.Condition,
                    DeviceId = iot.DeviceId
                };
            }
            return null;
        }
    }
}
