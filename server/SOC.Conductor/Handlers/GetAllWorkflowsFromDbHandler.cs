using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Models;
using SOC.Conductor.Models.Requests;

namespace SOC.Conductor.Handlers
{
    public record GetAllWorkflowsRequest() : IRequest<IEnumerable<WorkflowDto>> { }

    public class GetAllWorkflowsFromDbHandler
        : IRequestHandler<GetAllWorkflowsRequest, IEnumerable<WorkflowDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private IEnumerable<WorkflowResponseDto> _workflows;

        public GetAllWorkflowsFromDbHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<IEnumerable<WorkflowDto>> Handle(
            GetAllWorkflowsRequest request,
            CancellationToken cancellationToken
        )
        {
            _workflows = await _mediator.Send(new GetAllWorkflows(), cancellationToken);

            if (_workflows != null && !_workflows.Any())
                throw new Exception("There are no registered workflows on conductor");

            var res = await _unitOfWork.Workflows.GetAllAsync(cancellationToken);
            if (res is not null)
            {
                var dtos = res.Select(
                    workflow =>
                        new WorkflowDto
                        {
                            Id = workflow.Id,
                            Name = workflow.Name,
                            CreateDate = workflow.CreatedAt,
                            Version = workflow.Version,
                            Enabled = workflow.Enabled,
                            UpdateDate = workflow.UpdatedAt,
                            InputParameters = GetWfInputParams(workflow.Name)
                        }
                );
                return dtos;
            }
            else
                return Enumerable.Empty<WorkflowDto>();
        }

        private ICollection<string> GetWfInputParams(string workflowName)
        {
            var wf =
                _workflows.FirstOrDefault(wf => wf.Name == workflowName)
                ?? throw new Exception(
                    $"There is no workflow with name {workflowName} registered on conductor."
                );

            return wf.InputParameters.ToList();
        }
    }
}
