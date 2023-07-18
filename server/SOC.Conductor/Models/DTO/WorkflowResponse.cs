using System.ComponentModel.DataAnnotations;

namespace SOC.Conductor.Models.DTO
{
	public class WorkflowResponse 
	{
		public string Name { get; set; }
		public int Version { get; set; }
		public bool Enabled { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
