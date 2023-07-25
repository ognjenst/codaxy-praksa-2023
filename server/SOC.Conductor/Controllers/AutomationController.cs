using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;

namespace SOC.Conductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
        /// <summary>
        /// Create automation entity
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
            var automation = new AutomationDto
            {
                TriggerId = automationDto.TriggerId,
                WorkflowId = automationDto.WorkflowId,
                Name = automationDto.Name,
                InputParameters = automationDto.InputParameters,
            };

            await Task.Delay(2000);

            return Ok(automation);
        }
    }
}
