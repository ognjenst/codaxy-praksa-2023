using MediatR;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Models;

namespace SOC.Conductor.Handlers
{
    public record DeleteTriggerRequest(string Type, int TriggerId) : IRequest { }

    public class DeleteTriggerHandler : IRequestHandler<DeleteTriggerRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeleteTriggerHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task Handle(DeleteTriggerRequest request, CancellationToken cancellationToken)
        {
            if (request.Type == nameof(PeriodicTrigger))
            {
                var trigger = (
                    await _unitOfWork.PeriodicTriggers.GetByCondition(
                        x => x.Id == request.TriggerId,
                        cancellationToken
                    )
                ).FirstOrDefault();
                if (trigger is not null)
                {
                    var result = await _unitOfWork.PeriodicTriggers.DeleteAsync(
                        trigger,
                        cancellationToken
                    );
                    await _unitOfWork.SaveAllAsync();
                    await _mediator.Publish(
                        new TriggerNotification
                        {
                            Action = Models.Enums.TriggerNotificationAction.DELETE,
                            Trigger = trigger
                        },
                        cancellationToken
                    );
                }
            }
            if (request.Type == nameof(IoTTrigger))
            {
                var trigger = (
                    await _unitOfWork.IoTTriggers.GetByCondition(
                        x => x.Id == request.TriggerId,
                        cancellationToken
                    )
                ).FirstOrDefault();
                if (trigger is not null)
                {
                    var result = await _unitOfWork.IoTTriggers.DeleteAsync(
                        trigger,
                        cancellationToken
                    );
                    await _unitOfWork.SaveAllAsync();
                    await _mediator.Publish(
                        new TriggerNotification
                        {
                            Action = Models.Enums.TriggerNotificationAction.DELETE,
                            Trigger = trigger
                        },
                        cancellationToken
                    );
                }
            }
        }
    }
}
