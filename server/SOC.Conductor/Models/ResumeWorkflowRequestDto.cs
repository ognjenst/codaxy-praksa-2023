using Newtonsoft.Json;

namespace SOC.Conductor.Models
{
    public class ResumeWorkflowRequestDto
    {
        [JsonProperty("workflowId")]
        public string WorkflowId { get; set; }
    }
}
