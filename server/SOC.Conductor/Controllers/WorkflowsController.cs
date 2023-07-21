using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Generated;
using SOC.Conductor.Models;
using SOC.Conductor.Models.Requests;
using Task = System.Threading.Tasks.Task;

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

	[HttpGet("GetAllTasksAsync")]
	public async Task<IActionResult> GetAllTasksAsync()
	{
		return Ok(await _mediator.Send(new GetAllTasks()));
	}

	[HttpGet("GetAllWorkflowsAsync")]
	public async Task<IActionResult> GetAllWorkflows()
	{
		return Ok(await _mediator.Send(new GetAllWorkflows()));
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

	[HttpPost("PlayWorkflowAsync")]
	public async Task<IActionResult> PlayWorkflowAsync([FromBody] PlayRequestDto playDto)
	{
		await _mediator.Send(new PlayWorkflow(playDto));
		return Ok();
	}
}
