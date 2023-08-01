using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Handlers;
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
    /// Gets all workflows from a database.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetAllWorkflowsFromDB")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkflowDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
    public async Task<IActionResult> GetAllWorkflowsFromDBAsync()
    {
        var result = await _mediator.Send(new GetAllWorkflowsRequest());

        if (result is not null)
            return Ok(result);

        return NotFound();
    }

    /// <summary>
    /// Deletes a workflow.
    /// </summary>
    /// <param name="workflowName"></param>
    /// <param name="workflowVersion"></param>
    /// <returns></returns>
    [HttpDelete("{workflowName}/{workflowVersion}", Name = "DeleteWorkflow")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
    public async Task<IActionResult> DeleteWorkflowAsync([FromRoute] string workflowName, [FromRoute] int workflowVersion = 1)
    {
        await _mediator.Send(new DeleteWorkflowRequest(workflowName, workflowVersion));

        return NoContent();
    }

    /// <summary>
    /// Creates new workflow.
    /// </summary>
    /// <param name="createWorkflowDto"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateWorkflow")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkflowDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public async Task<IActionResult> CreateWorkflowAsync([FromBody] CreateWorkflowDto createWorkflowDto)
    {
        var result = await _mediator.Send(new CreateWorkflowRequest(createWorkflowDto));

        if (result is not null)
            return Ok(result);

        return NoContent();
    }


    /// <summary>
    /// Updates a workflow.
    /// </summary>
    /// <param name="workflowDto"></param>
    /// <returns></returns>
    [HttpPut(Name = "UpdateWorkflow")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkflowDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public async Task<IActionResult> UpdateWorkflowAsync([FromRoute] int workflowId, [FromBody] CreateWorkflowDto workflowDto)
    {
        var result = await _mediator.Send(new UpdateWorkflowRequest(workflowDto));
        
        if (result is not null)
            return Ok(result);

        return NotFound();
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

    /// <summary>
	/// Pauses workflow
	/// </summary>
	/// <param name="pauseDto"></param>
	/// <returns></returns>
    [HttpPut("PauseWorkflowAsync")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
	public async Task<IActionResult> PauseWorkflowAsync([FromBody] PauseWorkflowRequestDto pauseDto)
	{
		await _mediator.Send(new PauseWorkflow(pauseDto));

		return NoContent();
	}


    /// <summary>
	/// Resumes workflow
	/// </summary>
	/// <param name="resumeDto"></param>
	/// <returns></returns>
    [HttpPut("ResumeWorkflow")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
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
