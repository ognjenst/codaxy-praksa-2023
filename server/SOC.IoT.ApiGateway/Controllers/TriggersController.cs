using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Client.Generated;

namespace SOC.IoT.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : ControllerBase
    {
        private readonly ITriggersService _triggersService;

        public TriggersController(ITriggersService triggersService)
        {
            _triggersService = triggersService;
        }


        /// <summary>
        /// Returns all triggers.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllTriggersAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<CommonTriggerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllTriggersAsync()
        {
            var triggers = await _triggersService.GetAllTriggersAsync();

            await Task.Delay(1000);

            return Ok(triggers);
        }
    }
}
