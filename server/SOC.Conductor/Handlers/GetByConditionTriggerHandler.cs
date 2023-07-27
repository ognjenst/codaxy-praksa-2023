using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using System.Linq.Expressions;

namespace SOC.Conductor.Handlers
{
    public record GetByConditionTriggerRequest(Expression<Func<Trigger, bool>> condition) : IRequest<IEnumerable<Trigger>> { }

    public class GetByConditionTriggerHandler : IRequestHandler<GetByConditionTriggerRequest, IEnumerable<Trigger>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetByConditionTriggerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Trigger>> Handle(GetByConditionTriggerRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Triggers.GetByCondition(request.condition, cancellationToken);
        }
    }
}
