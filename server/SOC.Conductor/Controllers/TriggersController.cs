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
    public class TriggersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TriggersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all triggers.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllTriggers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Trigger))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllTriggersAsync()
        {
            var result = await _mediator.Send(new GetAllTriggersRequest());

            if (result is not null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Deletes a trigger.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpDelete(Name = "DeleteTrigger")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Trigger))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
        public async Task<IActionResult> DeleteTriggerAsync([FromBody] CommonTriggerDto entity)
        {
            var result = await _mediator.Send(new DeleteTriggerRequest(entity.Id));
            
            if (result is not null)
                return Ok(result);
            
            return NotFound();
        }


        /// <summary>
        /// Creates a trigger.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateTrigger")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Trigger))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
        public async Task<IActionResult> CreateTriggerAsync([FromBody] Trigger entity)
        {
            var result = await _mediator.Send(new CreateTriggerRequest(entity));

            if (result is not null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Updates a trigger.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut(Name = "UpdateTrigger")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Trigger))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
        public async Task<IActionResult> UpdateTriggerAsync([FromBody] Trigger entity)
        {
            var result = await _mediator.Send(new UpdateTriggerRequest(entity));
            if (result is not null)
                return Ok(result);

            return NotFound();
        }
    }
}
