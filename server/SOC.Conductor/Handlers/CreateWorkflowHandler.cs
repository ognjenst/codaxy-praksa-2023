using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record CreateWorkflowRequest(CreateWorkflowDto entity) : IRequest<Workflow> { }

    public class CreateWorkflowHandler : IRequestHandler<CreateWorkflowRequest, Workflow>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkflowHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Workflow> Handle(CreateWorkflowRequest request, CancellationToken cancellationToken)
        {
            var workflow = new Workflow()
            {
                Name = request.entity.Name,
                Version = request.entity.Version,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Enabled = true
            };
            
            // WorkflowBuilderService

            var result =  await _unitOfWork.Workflows.CreateAsync(workflow, cancellationToken);

            await _unitOfWork.SaveAllAsync();
            return result;
        }
    }
}
