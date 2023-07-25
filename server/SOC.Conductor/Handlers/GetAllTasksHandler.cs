using AutoMapper;
using MediatR;
using SOC.Conductor.Generated;
using SOC.Conductor.Models;
using SOC.Conductor.Models.Requests;

namespace SOC.Conductor.Handlers;

public class GetAllTasksHandler : IRequestHandler<GetAllTasks, IEnumerable<TaskResponseDto>>
{
    private readonly IMetadataResourceClient _client;
    private readonly IMapper _mapper;

    public GetAllTasksHandler(IMetadataResourceClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskResponseDto>> Handle(GetAllTasks request, CancellationToken cancellationToken)
    {
        var arrTasks = await _client.GetTaskDefsAsync();

        var arrReturn = _mapper.Map<IEnumerable<TaskResponseDto>>(arrTasks);

        return arrReturn;
    }
}
