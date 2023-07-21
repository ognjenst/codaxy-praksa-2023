using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
using SOC.Conductor.Handlers;
using System.Linq.Expressions;

namespace SOC.Conductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutomationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Returns all automations.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllAutomations")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Automation))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllAutomationsAsync()
        {
            var result = await _mediator.Send(new GetAllAutomationsRequest());

            //var dtos = result.Select(automation => new AutomationDto
            //{
            //    TriggerId = automation.TriggerId,
            //    WorkflowId = automation.WorkflowId,
            //}); ovo bi trebalo u handler-u napisati 

            if (result is not null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Deletes an automation.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpDelete(Name = "DeleteAutomation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Automation))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
        public async Task<IActionResult> DeleteAutomationAsync([FromBody] AutomationDto entity)
        {
            var result = await _mediator.Send(new DeleteAutomationRequest(entity.WorkflowId, entity.TriggerId));         

            if(result is not null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Insert a new automation into database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateAutomation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Automation))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
        public async Task<IActionResult> CreateAutomationAsync([FromBody] Automation entity)
        {
            var result = await _mediator.Send(new CreateAutomationRequest(entity));

            if (result is not null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Updates an automation.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut(Name = "UpdateAutomation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Automation))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]

        public async Task<IActionResult> UpdateAutomationAsync([FromBody] Automation entity)
        {
            var result = await _mediator.Send(new UpdateAutomationRequest(entity));
            if (result is not null) 
                return Ok(result);

            return NotFound();
        }      
    }
}
