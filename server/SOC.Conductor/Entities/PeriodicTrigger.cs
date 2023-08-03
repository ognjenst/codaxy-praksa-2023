using SOC.Conductor.Entities.Enums;

namespace SOC.Conductor.Entities
{
    public class PeriodicTrigger : Trigger
    {
        public DateTimeOffset Start { get; set; }
        public int Period { get; set; }
        public PeriodTriggerUnit Unit { get; set; }
    }
}
