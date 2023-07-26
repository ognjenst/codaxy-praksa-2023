using ConductorSharp.Client.Service;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Client.Generated;
using System.Net;

namespace SOC.IoT.ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkflowsController : ControllerBase
{
    private readonly IWorkflowsClient _workflowsClient;

    public WorkflowsController(IWorkflowsClient workflowsClient)
    {
        _workflowsClient = workflowsClient;
    }
    

    /// <summary>
    /// Returns all registered workflows from conductor.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetAllWorkflowsAsync")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<WorkflowResponseDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
    public async Task<IActionResult> GetAllWorkflowsAsync()
    {
        var data = await _workflowsClient.GetAllWorkflowsAsync();

        return Ok(data);
    }

	/// <summary>
	/// Returns all registered workflows from conductor.
	/// </summary>
	/// <returns></returns>
	[HttpGet("GetAllTasks", Name = "GetAllTasksAsync")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<TaskResponseDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
	public async Task<IActionResult> GetAllTasksAsync()
	{
        var data = await _workflowsClient.GetAllTasksAsync();

		return Ok(data);
	}

	/// <summary>
	/// Play workflow
	/// </summary>
	/// <param name="playDto"></param>
	/// <returns></returns>
	[HttpPost("PlayWorkflow", Name = "PlayWorkflow")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
	[ProducesResponseType(StatusCodes.Status201Created, Type = null)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
	public async Task<IActionResult> PlayWorkflowAsync([FromBody] PlayRequestDto playDto)
	{
		await _workflowsClient.PlayWorkflowAsync(playDto);
		return StatusCode(201);
	}
}
