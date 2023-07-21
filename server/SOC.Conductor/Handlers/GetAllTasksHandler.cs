using AutoMapper;
using MediatR;
using SOC.Conductor.Generated;
using SOC.Conductor.Models;
using SOC.Conductor.Models.Requests;

namespace SOC.Conductor.Handlers
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasks, List<TaskResponseDto>>
    {
        private readonly IMetadataResourceClient _client;
        private readonly IMapper _mapper;

        public GetAllTasksHandler(IMetadataResourceClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<List<TaskResponseDto>> Handle(GetAllTasks request, CancellationToken cancellationToken)
        {
            var arrTasks = await _client.GetTaskDefsAsync();

            var arrReturn = new List<TaskResponseDto>();
        }
    }
}
