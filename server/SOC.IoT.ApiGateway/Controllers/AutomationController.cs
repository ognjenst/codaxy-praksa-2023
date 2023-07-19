using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Client.Generated;

namespace SOC.IoT.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
        private readonly IAutomationService _automationService;

        public AutomationController(IAutomationService automationService)
        {
            _automationService = automationService;
        }

        /// <summary>
        /// Create automation entity
        /// </summary>
        /// <param name="automationDto"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateAutomationAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AutomationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
        public async Task<IActionResult> CreateAutomationAsync(AutomationDto automationDto)
        {
            var automation = await _automationService.CreateAutomationAsync(automationDto);

            await Task.Delay(1000);

            return Ok(automation);
        }
    }
}
