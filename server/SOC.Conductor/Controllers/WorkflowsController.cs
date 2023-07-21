using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Generated;
using SOC.Conductor.Handlers;
using System.Linq.Expressions;
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

    [HttpDelete(Name = "DeleteWorkflow")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Trigger))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
    public async Task<IActionResult> DeleteWorkflowAsync([FromBody] WorkflowDto entity)
    {
        var result = await _mediator.Send(new DeleteWorkflowRequest(entity.Id));

        if (result is not null)
            return Ok(result);

        return NotFound();
    }

    [HttpPost(Name = "CreateWorkflow")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Entities.Workflow))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public async Task<IActionResult> CreateWorkflowAsync([FromBody] CreateWorkflowDto entity)
    {
        var result = await _mediator.Send(new CreateWorkflowRequest(entity));

        if (result is not null)
            return Ok(result);

        return NotFound();
    }


    /// <summary>
    /// Updates a workflow.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPut(Name = "UpdateWorkflow")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Entities.Workflow))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public async Task<IActionResult> UpdateWorkflowAsync([FromBody] Entities.Workflow entity)
    {
        var result = await _mediator.Send(new UpdateWorkflowRequest(entity));
        if (result is not null)
            return Ok(result);

        return NotFound();
    }
}
