using Newtonsoft.Json;

namespace SOC.Conductor.Models
{
    public class PauseWorkflowRequestDto
    {
        [JsonProperty("workflowId")]
        public string WorkflowId { get; set; }
    }
}
