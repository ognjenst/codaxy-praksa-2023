using AutoMapper;
using MediatR;
using SOC.Conductor.Generated;
using SOC.Conductor.Models.Requests;

namespace SOC.Conductor.Handlers
{
    public class PlayWorkflowHandler : IRequestHandler<PlayWorkflow>
    {
        private readonly IWorkflowResourceClient _client;
        private readonly IMapper _mapper;

        public PlayWorkflowHandler(IWorkflowResourceClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task Handle(PlayWorkflow request, CancellationToken cancellationToken)
        {
            await _client.StartWorkflow_1Async(_mapper.Map<StartWorkflowRequest>(request.playDto));
        }
    }
}
