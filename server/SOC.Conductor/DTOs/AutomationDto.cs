using Newtonsoft.Json.Linq;

namespace SOC.Conductor.DTOs
{
    public sealed class AutomationDto
    {
        public int WorkflowId { get; set; }
        public int TriggerId { get; set; }
        public string Name { get; set; }
        public JObject InputParameters { get; set; }
    }
}
