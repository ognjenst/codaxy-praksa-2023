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
        [HttpGet]
        public async Task<IActionResult> GetAllTriggersAsync()
        {
            var triggers = await _triggersService.GetAllTriggersAsync();

            await Task.Delay(1000);

            return Ok(triggers);
        }
    }
}
