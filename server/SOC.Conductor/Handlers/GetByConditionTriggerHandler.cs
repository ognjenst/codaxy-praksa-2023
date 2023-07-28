using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using System.Linq.Expressions;

namespace SOC.Conductor.Handlers
{
    public record GetByConditionTriggerRequest(string Type, Expression<Func<PeriodicTrigger, bool>> periodicCondition, Expression<Func<IoTTrigger, bool>> iotCondition) : IRequest<IEnumerable<Trigger>> { }

    public class GetByConditionTriggerHandler : IRequestHandler<GetByConditionTriggerRequest, IEnumerable<Trigger>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetByConditionTriggerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Trigger>> Handle(GetByConditionTriggerRequest request, CancellationToken cancellationToken)
        {
            if (request.Type == nameof(PeriodicTrigger))
            {
                return await _unitOfWork.PeriodicTriggers.GetByCondition(request.periodicCondition, cancellationToken);
            }
            else if (request.Type == nameof(IoTTrigger))
            {
                return await _unitOfWork.IoTTriggers.GetByCondition(request.iotCondition, cancellationToken);
            }
            else
                return null;
        }
    }
}
