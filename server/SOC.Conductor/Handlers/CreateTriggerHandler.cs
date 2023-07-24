using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record CreateTriggerRequest(string Type, CommonTriggerDto commonTriggerDto) : IRequest<CommonTriggerDto> { }

    public class CreateTriggerHandler : IRequestHandler<CreateTriggerRequest, CommonTriggerDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTriggerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonTriggerDto> Handle(CreateTriggerRequest request, CancellationToken cancellationToken)
        {
            if (request.Type == "PeriodicTrigger")
            {
                var periodic = new PeriodicTrigger()
                {
                    Id = request.commonTriggerDto.Id,
                    Name = request.commonTriggerDto.Name,
                }; 

                var result = await _unitOfWork.PeriodicTriggers.CreateAsync(periodic);
                await _unitOfWork.SaveAllAsync();

                return new CommonTriggerDto()
                {
                    Id = request.commonTriggerDto.Id,
                    Name = request.commonTriggerDto.Name,
                };
            }

            if (request.Type == "IoTTrigger")
            {
                var iot = new IoTTrigger()
                {
                    Property = request.commonTriggerDto.Property,
                    Value = request.commonTriggerDto.Value,
                    Condition = request.commonTriggerDto.Condition,
                };

                var result = await _unitOfWork.IoTTriggers.CreateAsync(iot);
                await _unitOfWork.SaveAllAsync();

                return new CommonTriggerDto()
                {
                    Property = iot.Property,
                    Value = iot.Value,
                    Condition = iot.Condition,
                };
            }
            return null;
        }
    }
}
