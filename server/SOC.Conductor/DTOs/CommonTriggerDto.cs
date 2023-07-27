using SOC.Conductor.Entities.Enums;

namespace SOC.Conductor.DTOs
{
    public sealed class CommonTriggerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Property { get; set; }
        public string? Value { get; set; }
        public string? Condition { get; set; }
        public DateTime? Start { get; set; }
        public int? Period { get; set; }
        public  PeriodTriggerUnit? Unit { get; set; }
    }
}
