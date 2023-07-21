using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record UpdateTriggerRequest(Trigger entity) : IRequest<Trigger> { }

    public class UpdateTriggerHandler : IRequestHandler<UpdateTriggerRequest, Trigger>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTriggerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Trigger> Handle(UpdateTriggerRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Triggers.UpdateAsync(request.entity, cancellationToken);
        }
    }
}
