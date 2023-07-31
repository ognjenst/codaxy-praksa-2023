using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;
using SOC.Conductor.Services;

namespace SOC.Conductor.Handlers
{
    public record CreateWorkflowRequest(CreateWorkflowDto workflowDto) : IRequest<WorkflowDto> { }

    public class CreateWorkflowHandler : IRequestHandler<CreateWorkflowRequest, WorkflowDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkflowBuilderService _workflowBuilderService;
        private readonly ILogger<CreateWorkflowHandler> _logger;

        public CreateWorkflowHandler(IUnitOfWork unitOfWork, IWorkflowBuilderService workflowBuilderService, ILogger<CreateWorkflowHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _workflowBuilderService = workflowBuilderService;
            _logger = logger;
        }

        public async Task<WorkflowDto> Handle(CreateWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = new Workflow()
            {
                Name = request.workflowDto.Name,
                Version = request.workflowDto.Version,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Enabled = true
            };

            try
            {
                await _workflowBuilderService.Build(request.workflowDto);
                var result =  await _unitOfWork.Workflows.CreateAsync(workflow, cancellationToken);
                await _unitOfWork.SaveAllAsync();
                return new WorkflowDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Version = result.Version,
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    Enabled = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to register workflow into the database. {ex.Message}");
                return null;
            }
        }
    }
}
