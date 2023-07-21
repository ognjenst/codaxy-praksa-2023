using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;
using SOC.Conductor.Handlers;

namespace SOC.Conductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutomationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> GetAllAutomationAsync()
        {
            var result = await _mediator.Send(new GetAllAutomationsRequest());

            if(result is not null)
                return Ok(result);

            return NotFound();
        }


        /// <summary>
        /// Create automation entity
        /// </summary>
        /// <param name="automationDto"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateAutomationAysnc")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AutomationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
        public async Task<IActionResult> CreateAutomationAsync([FromBody] AutomationDto automationDto)
        {
            var automation = new AutomationDto
            {
                TriggerId = automationDto.TriggerId,
                WorkflowId = automationDto.WorkflowId
            };

            await Task.Delay(2000);

            return Ok(automation);
        }

    }
}
