using AutoMapper;
using MediatR;
using SOC.Conductor.Generated;
using SOC.Conductor.Models.Requests;
using SOC.Conductor.Models;

namespace SOC.Conductor.Handlers;

public class GetAllWorkflowsHandler : IRequestHandler<GetAllWorkflows, IEnumerable<WorkflowResponseDto>>
{
	private readonly IMetadataResourceClient _client;
	private readonly IMapper _mapper;

	public GetAllWorkflowsHandler(IMetadataResourceClient client, IMapper mapper)
	{
		_client = client;
		_mapper = mapper;
	}

	public async Task<IEnumerable<WorkflowResponseDto>> Handle(GetAllWorkflows request, CancellationToken cancellationToken)
	{
		var workflows = await _client.GetAllAsync();
		var result = workflows.Select(x => _mapper.Map<WorkflowResponseDto>(x)).ToList();
		return result;
	}
}
