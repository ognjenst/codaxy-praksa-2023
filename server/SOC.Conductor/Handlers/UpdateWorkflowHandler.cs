using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Services;

namespace SOC.Conductor.Handlers
{
    public record UpdateWorkflowRequest(CreateWorkflowDto workflowDto) : IRequest<WorkflowDto> { }

    public class UpdateWorkflowHandler : IRequestHandler<UpdateWorkflowRequest, WorkflowDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkflowBuilderService _workflowBuilderService;

        public UpdateWorkflowHandler(IUnitOfWork unitOfWork, IWorkflowBuilderService workflowBuilderService)
        {
            _unitOfWork = unitOfWork;
            _workflowBuilderService = workflowBuilderService;
        }

        public async Task<WorkflowDto> Handle(UpdateWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = (await _unitOfWork.Workflows.GetByCondition(x => x.Name == request.workflowDto.Name && x.Version == request.workflowDto.Version, cancellationToken)).FirstOrDefault();
            if (workflow is not null)
            {
                workflow.Name = request.workflowDto.Name;
                workflow.Version = request.workflowDto.Version;
                workflow.UpdatedAt = DateTime.UtcNow;    

                var result = await _unitOfWork.Workflows.UpdateAsync(workflow, cancellationToken);

                await _workflowBuilderService.Build(request.workflowDto);

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
