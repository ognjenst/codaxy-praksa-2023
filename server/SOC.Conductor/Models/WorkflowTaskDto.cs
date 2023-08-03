using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

		[JsonProperty("expression")]
		public string Expression { get; set; }

		[JsonProperty("decisionCases")]
		public Dictionary<string, ICollection<WorkflowTaskDto>>? DecisionCases { get; set; }
	}
}
