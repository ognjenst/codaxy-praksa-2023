﻿using SOC.Conductor.Entities.Enums;

namespace SOC.Conductor.DTOs
{
    public sealed class CommonTriggerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Property { get; set; }
        public string? Value { get; set; }
        public Operator? Condition { get; set; }
        public DateTimeOffset? Start { get; set; }
        public int? Period { get; set; }
        public PeriodTriggerUnit? Unit { get; set; }
        public string? DeviceId { get; set; }
    }
}
