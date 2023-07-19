using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.DTOs;

namespace SOC.Conductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TriggersController : ControllerBase
    {
        [HttpGet(Name = "GetAllTriggersAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<CommonTriggerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IActionResult> GetAllTriggersAsync()
        {
            var triggers = new List<CommonTriggerDto>
            {
                new CommonTriggerDto
                {
                    Id = 1,
                    Name = "Test",
                    Property = "Test",
                    Value = "Test",
                    Condition = "Test",
                },
                new CommonTriggerDto { 
                    Id = 2,
                    Name = "Test1",
                    Start = DateTime.UtcNow,
                    Period = 5,
                    Unit = Entities.Enums.PeriodTriggerUnit.SECONDS,
                }
            };

            await Task.Delay(2000);

            return Ok(triggers);

        }
    }
}
