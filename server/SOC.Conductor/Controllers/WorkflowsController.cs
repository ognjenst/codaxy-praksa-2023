﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

using SOC.Conductor.DTOs;
using SOC.Conductor.Generated;
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
	/// Get all tasks 
	/// </summary>
	/// <param name=""></param>
	/// <returns></returns>
	[HttpGet("GetAllTasks", Name = "GetAllTasks")]
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
	/// Get all workflows
	/// </summary>
	/// <param name=""></param>
	/// <returns></returns>
	[HttpGet("GetAllWorkflows", Name = "GetAllWorkflows")]
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

	[HttpPut("PauseWorkflowAsync")]
	public async Task<IActionResult> PauseWorkflowAsync([FromBody] PauseWorkflowRequestDto pauseDto)
	{
		await _mediator.Send(new PauseWorkflow(pauseDto));

		return NoContent();
	}

	[HttpPut("ResumeWorkflowAsync")]
	public async Task<IActionResult> ResumeWorkflowAsync([FromBody] ResumeWorkflowRequestDto resumeDto)
	{
		await _mediator.Send(new ResumeWorkflow(resumeDto));

		return NoContent();
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
		await _mediator.Send(new PlayWorkflow(playDto));
		return Ok();
	}
}
