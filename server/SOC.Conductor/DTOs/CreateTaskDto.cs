﻿using Newtonsoft.Json.Linq;

namespace SOC.Conductor.DTOs
{
    public class CreateTaskDto
    {
        public string Name { get; set; }
        public string TaskReferenceName { get; set; }
        public JObject InputParameters { get; set; }
        public string Type { get; set; }
    }
}
