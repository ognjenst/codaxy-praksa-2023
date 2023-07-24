using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record GetAllWorkflowsRequest() : IRequest<IEnumerable<WorkflowDto>> { }

    public class GetAllWorkflowHandler : IRequestHandler<GetAllWorkflowsRequest, IEnumerable<WorkflowDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWorkflowHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<WorkflowDto>> Handle(GetAllWorkflowsRequest request, CancellationToken cancellationToken)
        {
            var res = await _unitOfWork.Workflows.GetAllAsync(cancellationToken);
            var dtos = res.Select(workflow => new WorkflowDto
            {
                Id = workflow.Id,
                Name = workflow.Name,
                CreateDate = workflow.CreatedAt,
                Version = workflow.Version,
                Enabled = workflow.Enabled,
                UpdateDate = workflow.UpdatedAt,               
            });
            return dtos;
        }
    }
}
