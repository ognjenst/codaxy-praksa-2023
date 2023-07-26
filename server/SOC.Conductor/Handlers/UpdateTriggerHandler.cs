﻿using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record UpdateTriggerRequest(string type, int triggerId, CommonTriggerDto commonTriggerDto) : IRequest<CommonTriggerDto> { }

    public class UpdateTriggerHandler : IRequestHandler<UpdateTriggerRequest, CommonTriggerDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTriggerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonTriggerDto> Handle(UpdateTriggerRequest request, CancellationToken cancellationToken)
        {
            var trigger = (await _unitOfWork.Triggers.GetByCondition(x => x.Id == request.triggerId, cancellationToken)).FirstOrDefault();
            if (trigger is not null)
            {
                if (request.type == nameof(PeriodicTrigger))
                {
                    var periodicTrigger = trigger as PeriodicTrigger;
                    periodicTrigger.Id = request.commonTriggerDto.Id;
                    periodicTrigger.Name = request.commonTriggerDto.Name;
                    periodicTrigger.Period = request.commonTriggerDto.Period.GetValueOrDefault();
                    periodicTrigger.Start = request.commonTriggerDto.Start.GetValueOrDefault();
                    periodicTrigger.Unit = request.commonTriggerDto.Unit.GetValueOrDefault();

                    var result = await _unitOfWork.PeriodicTriggers.UpdateAsync(periodicTrigger, cancellationToken);
                    await _unitOfWork.SaveAllAsync();

                    return new CommonTriggerDto()
                    {
                        Id = periodicTrigger.Id,
                        Name = periodicTrigger.Name,
                        Period = periodicTrigger.Period,
                        Start = periodicTrigger.Start,
                        Unit = periodicTrigger.Unit,
                    };
                }

                else if (request.type == nameof(IoTTrigger))
                {
                    var iotTrigger = trigger as IoTTrigger;
                    iotTrigger.Id = request.commonTriggerDto.Id;
                    iotTrigger.Name = request.commonTriggerDto.Name;
                    iotTrigger.Property = request.commonTriggerDto.Property;
                    iotTrigger.Value = request.commonTriggerDto.Value;
                    iotTrigger.Condition = request.commonTriggerDto.Condition;

                    var result = await _unitOfWork.IoTTriggers.UpdateAsync(iotTrigger, cancellationToken);
                    await _unitOfWork.SaveAllAsync();

                    return new CommonTriggerDto()
                    {
                        Id = iotTrigger.Id,
                        Name = iotTrigger.Name,
                        Property = iotTrigger.Property,
                        Value = iotTrigger.Value,
                        Condition = iotTrigger.Condition,
                    };
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}