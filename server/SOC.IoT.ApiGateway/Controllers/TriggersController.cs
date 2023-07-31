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
        [HttpGet("{type}", Name = "GetAllTriggers")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(ICollection<CommonTriggerDto>)
        )]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllTriggersAsync([FromRoute] string type)
        {
            var triggers = await _triggersClient.GetAllTriggersAsync(type);

            return Ok(triggers);
        }
    }
}
