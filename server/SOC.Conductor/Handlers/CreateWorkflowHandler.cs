using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;
using SOC.Conductor.Services;

namespace SOC.Conductor.Handlers
{
    public record CreateWorkflowRequest(CreateWorkflowDto workflowDto) : IRequest<Workflow> { }

    public class CreateWorkflowHandler : IRequestHandler<CreateWorkflowRequest, Workflow>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkflowBuilderService _workflowBuilderService;

        public CreateWorkflowHandler(IUnitOfWork unitOfWork, IWorkflowBuilderService workflowBuilderService)
        {
            _unitOfWork = unitOfWork;
            _workflowBuilderService = workflowBuilderService;
        }

        public async Task<Workflow> Handle(CreateWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = new Workflow()
            {
                Name = request.workflowDto.Name,
                Version = request.workflowDto.Version,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Enabled = true
            };

            await _workflowBuilderService.Build(request.workflowDto);

            var result =  await _unitOfWork.Workflows.CreateAsync(workflow, cancellationToken);

            await _unitOfWork.SaveAllAsync();
            
            return result;
        }
    }
}
