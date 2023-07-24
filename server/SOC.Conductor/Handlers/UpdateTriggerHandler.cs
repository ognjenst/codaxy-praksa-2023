using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record UpdateTriggerRequest(string type, CommonTriggerDto commonTriggerDto) : IRequest<CommonTriggerDto> { }

    public class UpdateTriggerHandler : IRequestHandler<UpdateTriggerRequest, CommonTriggerDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTriggerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonTriggerDto> Handle(UpdateTriggerRequest request, CancellationToken cancellationToken)
        {
            if (request.type == "PeriodicTrigger")
            {
                var periodic = new PeriodicTrigger()
                {
                    Id = request.commonTriggerDto.Id,
                    Name = request.commonTriggerDto.Name,
                };

                var result = await _unitOfWork.PeriodicTriggers.UpdateAsync(periodic, cancellationToken);
                await _unitOfWork.SaveAllAsync();

                return new CommonTriggerDto()
                {
                    Id = request.commonTriggerDto.Id,
                    Name = request.commonTriggerDto.Name,
                };
            }

            if (request.type == "IoTTrigger")
            {
                var iot = new IoTTrigger()
                {
                    Property = request.commonTriggerDto.Property,
                    Value = request.commonTriggerDto.Value,
                    Condition = request.commonTriggerDto.Condition,
                };

                var result = await _unitOfWork.IoTTriggers.UpdateAsync(iot, cancellationToken);
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
