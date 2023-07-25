using MediatR;

namespace SOC.Conductor.Models.Requests
{
    public record PlayWorkflow(PlayRequestDto playDto) : IRequest;
}
