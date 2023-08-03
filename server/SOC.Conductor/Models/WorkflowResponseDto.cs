using Newtonsoft.Json;

namespace SOC.Conductor.Models
{
    public class WorkflowResponseDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("tasks")]
        public IEnumerable<WorkflowTaskDto> Tasks { get; set; }
        [JsonProperty("inputParameters")]
        public IEnumerable<string> InputParameters { get; set; }
    } 
}
