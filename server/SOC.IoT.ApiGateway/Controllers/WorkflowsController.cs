﻿using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Client.Generated;

namespace SOC.IoT.ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkflowsController : ControllerBase
{
    private readonly IWorkflowsService _workflowsService;

    public WorkflowsController(IWorkflowsService workflowsService)
    {
        _workflowsService = workflowsService;
    }

    /// <summary>
    /// Returns all registered workflows from conductor.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetAllWorkflowsAsync")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkflowDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
    public async Task<IActionResult> GetAllWorkflowsAsync()
    {
        var data = await _workflowsService.WorkflowsAsync();

        return Ok(data);
    }
}