using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Generated;
using Task = System.Threading.Tasks.Task;

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
