using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Generated;

namespace SOC.Conductor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkflowsController : ControllerBase
{
	private readonly IMetadataResourceClient _client;
	public WorkflowsController(IMetadataResourceClient client)
	{
		_client = client;
	}
	/// <summary>
	/// Returns all registered workflows from conductor.
	/// </summary>
	/// <returns></returns>
	[HttpGet(Name = "GetAllWorkflowsAsync")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<WorkflowDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
	public async Task<IActionResult> GetAllWorkflowsAsync()
	{
		var wf = new List<WorkflowDto>
		{
			new WorkflowDto
			{
				Id = 1,
				Name = "IOT_scan_hosts",
				Version = 1,
				CreateDate = DateTime.UtcNow,
				UpdateDate = DateTime.UtcNow,
				Enabled = true,
			},
			new WorkflowDto
			{
				Id = 2,
				Name = "IOT_set_morning_routine",
				Version = 1,
				CreateDate = DateTime.UtcNow,
				UpdateDate = DateTime.UtcNow,
				Enabled = true,
			}
		};

		await System.Threading.Tasks.Task.Delay(2000);

		return Ok(wf);
	}

	[HttpGet("GetAllTasksAsync")]
	public async Task<IActionResult> GetAllTasksAsync()
	{
		return Ok(await _client.GetTaskDefsAsync());
	}

	[HttpGet("GetAllWorkflowsAsync")]
	public async Task<IActionResult> GetAllWorkflows()
	{
		return Ok(await _client.GetAllAsync());
	}
}
