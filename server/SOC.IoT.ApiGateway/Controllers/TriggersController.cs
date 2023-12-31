﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Client.Generated;

namespace SOC.IoT.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : ControllerBase
    {
        private readonly ITriggersClient _triggersClient;

        public TriggersController(ITriggersClient triggersClient)
        {
            _triggersClient = triggersClient;
        }

        /// <summary>
        /// Returns all triggers.
        /// </summary>
        /// <returns></returns>
        //[Authorize(policy: "Read-Trigger")]
        [HttpGet("{type}",Name = "GetAllTriggersAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<CommonTriggerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllTriggersAsync([FromRoute] string type)
        {
            var triggers = await _triggersClient.GetAllTriggersAsync(type);

            return Ok(triggers);
        }

        /// <summary>
        /// Creates a trigger.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="commonTriggerDto"></param>
        /// <returns></returns>
        //[Authorize(policy: "Create-Trigger")]
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
            var result = await _triggersClient.CreateTriggerAsync(type, commonTriggerDto);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Deletes a trigger.
        /// </summary>
        /// <param name="triggerId"></param>
        /// <returns></returns>
        [HttpDelete("{type}/{triggerId}", Name = "DeleteTrigger")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = null)]
        public async Task<IActionResult> DeleteTriggerAsync([FromRoute] string type, int triggerId)
        {
            await _triggersClient.DeleteTriggerAsync(type, triggerId);

            return NoContent();
        }  
    }
}
