﻿using MediatR;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<WorkflowDto>))]
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
    /// <param name="workflowId"></param>
    /// <returns></returns>
    [HttpDelete("{workflowId}", Name = "DeleteWorkflow")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkflowDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
    public async Task<IActionResult> DeleteWorkflowAsync(int workflowId)
    {
        await _mediator.Send(new DeleteWorkflowRequest(workflowId));

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

        return NotFound();
    }


    /// <summary>
    /// Updates a workflow.
    /// </summary>
    /// <param name="workflowDto"></param>
    /// <returns></returns>
    [HttpPut("{workflowId}", Name = "UpdateWorkflow")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkflowDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public async Task<IActionResult> UpdateWorkflowAsync([FromRoute] int workflowId, [FromBody] WorkflowDto workflowDto)
    {
        workflowDto.Id = workflowId;
        var result = await _mediator.Send(new UpdateWorkflowRequest(workflowId, workflowDto));
        
        if (result is not null)
            return Ok(result);

        return NotFound();
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
