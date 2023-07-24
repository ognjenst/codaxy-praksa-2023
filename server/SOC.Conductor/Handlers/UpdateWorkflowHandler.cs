using MediatR;
using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record UpdateWorkflowRequest(WorkflowDto workflowDto) : IRequest<WorkflowDto> { }

    public class UpdateWorkflowHandler : IRequestHandler<UpdateWorkflowRequest, WorkflowDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkflowHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;          
        }

        public async Task<WorkflowDto> Handle(UpdateWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = new Workflow()
            {
                Id = request.workflowDto.Id,
                Name = request.workflowDto.Name,
                Version = request.workflowDto.Version,
                CreatedAt = request.workflowDto.CreateDate,
                UpdatedAt = request.workflowDto.UpdateDate,
                Enabled = request.workflowDto.Enabled,
            };

            var result = await _unitOfWork.Workflows.UpdateAsync(workflow, cancellationToken);

            await _unitOfWork.SaveAllAsync();
            return new WorkflowDto()
            {
                Id = result.Id,
                Name = result.Name,
                Version = result.Version,
                CreateDate = result.CreatedAt,
                UpdateDate = result.UpdatedAt,
                Enabled = result.Enabled,
            };
        }
    }
}
