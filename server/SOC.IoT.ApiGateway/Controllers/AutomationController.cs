using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Client.Generated;
using Microsoft.AspNetCore.Authorization;

namespace SOC.IoT.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
        private readonly IAutomationClient _automationClient;

        public AutomationController(IAutomationClient automationClient)
        {
            _automationClient = automationClient;
        }

		/// <summary>
		/// Create automation entity
		/// </summary>
		/// <param name="automationDto"></param>
		/// <returns></returns>
		[Authorize(policy: "Create-Automation")]
		[HttpPost(Name = "CreateAutomationAsync")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AutomationDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
		[ProducesResponseType(StatusCodes.Status201Created, Type = null)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
		public async Task<IActionResult> CreateAutomationAsync(AutomationDto automationDto)
		{
			var automation = await _automationClient.CreateAutomationAsync(automationDto);

			await Task.Delay(1000);

			return Ok(automation);
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
            var result = await _automationClient.GetAllAutomationsAsync();

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

    }
}
