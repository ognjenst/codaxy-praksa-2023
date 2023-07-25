using Newtonsoft.Json.Linq;

namespace SOC.Conductor.Entities
{
    public class Automation
    {
        public int WorkflowId { get; set; }
        public int TriggerId { get; set; }
        public string Name { get; set; }
        public JObject? InputParameters { get; set; }
    }
}
