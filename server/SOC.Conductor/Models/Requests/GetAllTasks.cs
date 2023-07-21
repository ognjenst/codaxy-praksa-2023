using MediatR;

namespace SOC.Conductor.Models.Requests
{
    public record GetAllTasks() : IRequest<IEnumerable<TaskResponseDto>>;
}
