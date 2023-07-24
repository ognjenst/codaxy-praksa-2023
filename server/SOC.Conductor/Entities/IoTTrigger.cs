namespace SOC.Conductor.Entities
{
    public class IoTTrigger : Trigger
    {
        public string Property { get; set; }
        public string Value { get; set; }
        public string Condition { get; set; } // Operator TODO: change to enum
    }
}
