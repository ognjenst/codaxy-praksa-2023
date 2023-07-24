using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Handlers
{
    public record DeleteTriggerRequest(int TriggerId) : IRequest { }

    public class DeleteTriggerHandler : IRequestHandler<DeleteTriggerRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTriggerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteTriggerRequest request, CancellationToken cancellationToken)
        {
            var trigger = (await _unitOfWork.Triggers.GetByCondition(x => x.Id == request.TriggerId, cancellationToken)).FirstOrDefault();
            if (trigger is not null) 
            {
                var result = await _unitOfWork.Triggers.DeleteAsync(trigger, cancellationToken);
                await _unitOfWork.SaveAllAsync();
            }
        }
    }
}
