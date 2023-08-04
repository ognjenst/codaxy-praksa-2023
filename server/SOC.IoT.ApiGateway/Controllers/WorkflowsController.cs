using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Client.Generated;

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
    [Authorize(policy: "Read-Workflow")]
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
    /// Gets all workflows from a database.
    /// </summary>
    /// <returns></returns>
    [HttpGet("db", Name = "GetAllWorkflowsFromDb")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<WorkflowDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
    public async Task<IActionResult> GetAllWorkflowsFromDBAsync()
    {
        var data = await _workflowsClient.GetAllWorkflowsFromDBAsync();

        return Ok(data);
    }

    /// <summary>
    /// Returns all registered workflows from conductor.
    /// </summary>
    /// <returns></returns>
    [Authorize(policy: "Read-Workflow")]
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
    [Authorize(policy: "Update-Workflow")]
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

    /// <summary>
    /// Deletes a workflow.
    /// </summary>
    /// <param name="workflowName"></param>
    /// <param name="workflowVersion"></param>
    /// <returns></returns>
    [Authorize(policy: "Delete-Workflow")]
    [HttpDelete("{workflowName}/{workflowVersion}", Name = "DeleteWorkflow")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    public async Task<IActionResult> DeleteWorkflowAsync(
        [FromRoute] string workflowName,
        [FromRoute] int workflowVersion = 1
    )
    {
        await _workflowsClient.DeleteWorkflowAsync(workflowName, workflowVersion);

        return NoContent();
    }

    /// <summary>
    /// Creates new workflow.
    /// </summary>
    /// <param name="createWorkflowDto"></param>
    /// <returns></returns>
    [Authorize(policy: "Create-Workflow")]
    [HttpPost(Name = "CreateWorkflow")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
    public async Task<IActionResult> CreateWorkflowAsync(
        [FromBody] CreateWorkflowDto createWorkflowDto
    )
    {
        var result = await _workflowsClient.CreateWorkflowAsync(createWorkflowDto);

        return StatusCode(201);
        ;
    }

    ///// <summary>
    ///// Updates a workflow.
    ///// </summary>
    ///// <param name="workflowDto"></param>
    ///// <returns></returns>
    //[Authorize(policy: "Update-Workflow")]
    //[HttpPut(Name = "UpdateWorkflow")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkflowDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = null)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    //public async Task<IActionResult> UpdateWorkflowAsync([FromBody] CreateWorkflowDto workflowDto)
    //{
    //    var result = await _workflowsClient.UpdateWorkflowAsync(workflowDto);

    //    if (result is not null)
    //        return Ok(result);

    //    return NotFound();
    //}
}
