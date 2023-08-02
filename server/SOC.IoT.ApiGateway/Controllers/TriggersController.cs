using Microsoft.AspNetCore.Authorization;
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
        [Authorize(policy: "Read-Trigger")]
        [HttpGet(Name = "GetAllTriggersAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<CommonTriggerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllTriggersAsync()
        {
            var triggers = await _triggersClient.GetAllTriggersAsync();

            await Task.Delay(1000);

            return Ok(triggers);
        }
    }
}
