using MediatR;
using SOC.Conductor.Contracts;

namespace SOC.Conductor.Handlers
{
    public record DeleteTriggerRequest(int TriggerId) : IRequest { }

    public class DeleteTriggerHandler : IRequestHandler<DeleteTriggerRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTriggerHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task Handle(DeleteTriggerRequest request, CancellationToken cancellationToken)
        {
            var trigger = (await _unitOfWork.Triggers.GetByCondition(x => x.Id == request.TriggerId, cancellationToken)).FirstOrDefault();
            if (trigger is not null) 
            {
                await _unitOfWork.Triggers.DeleteAsync(trigger, cancellationToken);
                await _unitOfWork.SaveAllAsync();
            }
        }
    }
}
