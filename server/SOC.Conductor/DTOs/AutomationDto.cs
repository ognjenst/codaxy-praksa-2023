using Newtonsoft.Json.Linq;

namespace SOC.Conductor.DTOs
{
    public sealed class AutomationDto
    {
        public int Id { get; set; }
        public int WorkflowId { get; set; }
        public int TriggerId { get; set; }
        public string Name { get; set; }
        public string? InputParameters { get; set; }
        public WorkflowDto? Workflow { get; set; }
        public CommonTriggerDto? Trigger { get; set; }
    }
}
