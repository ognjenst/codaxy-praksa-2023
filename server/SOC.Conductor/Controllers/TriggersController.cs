using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Handlers;

namespace SOC.Conductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TriggersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all triggers based on type(Periodic or IoT).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("{type}", Name = "GetAllTriggers")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CommonTriggerDto>)
        )]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllTriggersAsync([FromRoute] string type)
        {
            var result = await _mediator.Send(new GetAllTriggersRequest(type));

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        // TODO: Send TriggerNotification

        /// <summary>
        /// Deletes a trigger.
        /// </summary>
        /// <param name="triggerId"></param>
        /// <returns></returns>
        [HttpDelete("{type}/{triggerId}", Name = "DeleteTrigger")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        public async Task<IActionResult> DeleteTriggerAsync([FromRoute] string type, int triggerId)
        {
            await _mediator.Send(new DeleteTriggerRequest(type, triggerId));

            return NoContent();
        }

        /// <summary>
        /// Creates a trigger.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commonTriggerDto"></param>
        /// <returns></returns>
        [HttpPost("{type}", Name = "CreateTrigger")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonTriggerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
        public async Task<IActionResult> CreateTriggerAsync(
            [FromRoute] string type,
            [FromBody] CommonTriggerDto commonTriggerDto
        )
        {
            var result = await _mediator.Send(new CreateTriggerRequest(type, commonTriggerDto));

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Updates a trigger.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commonTriggerDto"></param>
        /// <returns></returns>
        [HttpPut("{type}/{triggerId}", Name = "UpdateTrigger")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonTriggerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
        public async Task<IActionResult> UpdateTriggerAsync(
            [FromRoute] string type,
            [FromRoute] int triggerId,
            [FromBody] CommonTriggerDto commonTriggerDto
        )
        {
            commonTriggerDto.Id = triggerId;
            var result = await _mediator.Send(
                new UpdateTriggerRequest(type, triggerId, commonTriggerDto)
            );
            if (result is not null)
                return Ok(result);

            return NotFound();
        }
    }
}
