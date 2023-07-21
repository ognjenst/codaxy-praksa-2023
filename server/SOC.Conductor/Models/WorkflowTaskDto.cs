using Newtonsoft.Json;

namespace SOC.Conductor.Models
{
	public class WorkflowTaskDto
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("taskReferenceName")]
		public string TaskReferenceName { get; set; }
		
		[JsonProperty("inputParameters")]
		public Dictionary<string, string> InputParameters { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
