﻿using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using System.Linq.Expressions;

namespace SOC.Conductor.Handlers
{
    public record GetByConditionAutomationsRequest(Expression<Func<Automation, bool>> condition) : IRequest<IEnumerable<Automation>> { }

    public class GetByConditionAutomationHandler : IRequestHandler<GetByConditionAutomationsRequest, IEnumerable<Automation>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetByConditionAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Automation>> Handle(GetByConditionAutomationsRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Automations.GetByCondition(request.condition, cancellationToken);
        }
    }
}
