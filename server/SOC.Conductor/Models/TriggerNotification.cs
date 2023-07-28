using MediatR;
using SOC.Conductor.Entities;
using SOC.Conductor.Models.Enums;

namespace SOC.Conductor.Models
{
    public class TriggerNotification : INotification
    {
        public required Trigger Trigger { get; set; }
        public required TriggerNotificationAction Action { get; set; }
    }
}
