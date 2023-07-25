using AutoMapper;
using MediatR;
using SOC.Conductor.Generated;
using SOC.Conductor.Models.Requests;

namespace SOC.Conductor.Handlers;

using Task = System.Threading.Tasks.Task;
public class PlayWorkflowHandler : IRequestHandler<PlayWorkflow>
{
    private readonly IWorkflowResourceClient _client;
    private readonly IMapper _mapper;

    public PlayWorkflowHandler(IWorkflowResourceClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task Handle(PlayWorkflow request, CancellationToken cancellationToken)
    {
        await _client.StartWorkflow_1Async(_mapper.Map<StartWorkflowRequest>(request.playDto));
    }
}
