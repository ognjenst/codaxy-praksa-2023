using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record GetAllAutomationsRequest() : IRequest<IEnumerable<Automation>> { }

    public class GetAllAutomationHandler : IRequestHandler<GetAllAutomationsRequest, IEnumerable<Automation>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAutomationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Automation>> Handle(GetAllAutomationsRequest request, CancellationToken cancellationToken)
        {
            // uraditi mapiranje u dto
            return await _unitOfWork.Automations.GetAllAsync(cancellationToken);
        }
    }
}
