﻿using Newtonsoft.Json.Linq;
using SOC.Conductor.DTOs;
namespace SOC.Conductor.DTOs;

public class CreateTaskDto
{
    public string Name { get; set; }
    public string TaskReferenceName { get; set; }
    public Dictionary<string, string> InputParameters { get; set; }
    public Dictionary<string, string>? ConditionInputParameters { get; set; }
    public string? Expression { get; set; }
}
