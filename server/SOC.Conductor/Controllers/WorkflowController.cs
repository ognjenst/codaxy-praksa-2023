using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Models.DTO;

namespace SOC.Conductor.Controllers
{
	
	public class WorkflowController : ControllerBase
	{

		public WorkflowController() { }

		[HttpGet("workflows")]
		public List<WorkflowResponse> GetAllWorkflows() 
		{
			var workflows = new List<WorkflowResponse>
			{
				new WorkflowResponse { Name = "Morning routine" },
				new WorkflowResponse { Name = "Mail received" },
				new WorkflowResponse { Name = "Locked lab mode" },
			};

			return workflows;	
		}
	}
}
