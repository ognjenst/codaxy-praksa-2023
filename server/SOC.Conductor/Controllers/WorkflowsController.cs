using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Models;
using SOC.Conductor.Models.Requests;

namespace SOC.Conductor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkflowsController : ControllerBase
{
	private readonly IMediator _mediator;

	public WorkflowsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	/// <summary>
	/// Create automation entity
	/// </summary>
	/// <param name="automationDto"></param>
	/// <returns></returns>
	[HttpGet("GetAllTasksAsync", Name = "GetAllTasksAsync")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<TaskResponseDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
	[ProducesResponseType(StatusCodes.Status201Created, Type = null)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
	public async Task<IActionResult> GetAllTasksAsync()
	{
		var response = await _mediator.Send(new GetAllTasks());
		if (response == null) return NotFound();
		return Ok(response);
	}

	/// <summary>
	/// Create automation entity
	/// </summary>
	/// <param name="automationDto"></param>
	/// <returns></returns>
	[HttpGet("GetAllWorkflows", Name = "GetAllWorkflowsAsync")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<WorkflowResponseDto>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
	[ProducesResponseType(StatusCodes.Status201Created, Type = null)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
	public async Task<IActionResult> GetAllWorkflows()
	{
		var response = await _mediator.Send(new GetAllWorkflows());
		if (response == null) return NotFound();
		return Ok(response);
	}
}
