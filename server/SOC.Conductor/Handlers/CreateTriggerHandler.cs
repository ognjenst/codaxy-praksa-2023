using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record CreateTriggerRequest(Trigger entity) : IRequest<Trigger> { }

    public class CreateTriggerHandler : IRequestHandler<CreateTriggerRequest, Trigger>
    {
        private IUnitOfWork _unitOfWork;

        public CreateTriggerHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Trigger> Handle(CreateTriggerRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Triggers.CreateAsync(request.entity, cancellationToken);
        }
    }
}
