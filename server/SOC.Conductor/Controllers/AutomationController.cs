using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Handlers;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AutomationDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllAutomationsAsync()
        {
            var result = await _mediator.Send(new GetAllAutomationsRequest()); 

            if (result is not null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Deletes an automation.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "DeleteAutomation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
        public async Task<IActionResult> DeleteAutomationAsync([FromRoute] int id)
        {
            await _mediator.Send(new DeleteAutomationRequest(id)); 
            
            return NoContent();
        }


        /// <summary>
        /// Insert a new automation into database.
        /// </summary>
        /// <param name="automationDto"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateAutomation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AutomationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
        public async Task<IActionResult> CreateAutomationAsync([FromBody] AutomationDto automationDto)
        {
            var result = await _mediator.Send(new CreateAutomationRequest(automationDto));

            if (result is not null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Updates an automation.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="automationDto"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdateAutomation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AutomationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]

        public async Task<IActionResult> UpdateAutomationAsync([FromRoute] int id, [FromBody] AutomationDto automationDto)
        {
            automationDto.Id = id;
            var result = await _mediator.Send(new UpdateAutomationRequest(id, automationDto));
            if (result is not null) 
                return Ok(result);

            return NotFound();
        }      
    }
}
