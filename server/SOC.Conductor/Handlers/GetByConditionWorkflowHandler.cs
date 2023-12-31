﻿using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using System.Linq.Expressions;

namespace SOC.Conductor.Handlers
{
    public record GetByConditionWorkflowRequest(Expression<Func<Workflow, bool>> condition) : IRequest<IEnumerable<Workflow>> { }

    public class GetByConditionWorkflowHandler : IRequestHandler<GetByConditionWorkflowRequest, IEnumerable<Workflow>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetByConditionWorkflowHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Workflow>> Handle(GetByConditionWorkflowRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Workflows.GetByCondition(request.condition, cancellationToken);
        }
    }
}
