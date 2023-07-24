using SOC.Conductor.Entities.Enums;

namespace SOC.Conductor.Entities
{
    public class IoTTrigger : Trigger
    {
        public string Property { get; set; }
        public string Value { get; set; }
        public Operator Condition { get; set; } // Operator TODO: change to enum Migration
    }
}
