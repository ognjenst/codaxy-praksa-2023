using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;

namespace SOC.Conductor.Handlers
{
    public record UpdateWorkflowRequest(int workflowId, WorkflowDto workflowDto) : IRequest<WorkflowDto> { }

    public class UpdateWorkflowHandler : IRequestHandler<UpdateWorkflowRequest, WorkflowDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkflowHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;          
        }

        public async Task<WorkflowDto> Handle(UpdateWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = (await _unitOfWork.Workflows.GetByCondition(x => x.Id == request.workflowId, cancellationToken)).FirstOrDefault();
            if (workflow is not null)
            {
                workflow.Name = request.workflowDto.Name;
                workflow.Version = request.workflowDto.Version;
                workflow.CreatedAt = request.workflowDto.CreateDate;
                workflow.UpdatedAt = request.workflowDto.UpdateDate;
                workflow.Enabled = request.workflowDto.Enabled;          

                var result = await _unitOfWork.Workflows.UpdateAsync(workflow, cancellationToken);

                await _unitOfWork.SaveAllAsync();
                return new WorkflowDto()
                {
                    Id = workflow.Id,
                    Name = result.Name,
                    Version = result.Version,
                    CreateDate = result.CreatedAt,
                    UpdateDate = result.UpdatedAt,
                    Enabled = result.Enabled,
                };
            }
            else
                return null;
        }
    }
}
