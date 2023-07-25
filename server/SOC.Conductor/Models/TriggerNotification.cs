using MediatR;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Models
{
    public class TriggerNotification : INotification
    {
        public required Trigger Trigger { get; set; }
        public required bool Deleted { get; set; }
    }
}
