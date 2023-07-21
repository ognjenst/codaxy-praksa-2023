using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Repositories;

namespace SOC.Conductor.Handlers
{
    public record GetAllTriggersRequest() : IRequest<IEnumerable<Trigger>> { }

    public class GetAllTriggerHandler : IRequestHandler<GetAllTriggersRequest, IEnumerable<Trigger>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTriggerHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Trigger>> Handle(GetAllTriggersRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Triggers.GetAllAsync(cancellationToken);
        }
    }
}
