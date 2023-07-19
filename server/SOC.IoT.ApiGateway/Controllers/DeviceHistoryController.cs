using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOC.IoT.ApiGateway.Controllers.Examples;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.ApiGateway.Models;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SOC.IoT.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DeviceHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        private const string _deviceIdRegexPattern = "0[xX][0-9a-fA-F]+";

        public DeviceHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Returns a single device with its last known state
        /// </summary>
        /// <param name="id">ID of the device</param>
        /// <returns>Single device with its last known state</returns>
        [HttpGet("{id}", Name = "GetDeviceHistory")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeviceExample))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceHistoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IResult> GetDevice([FromRoute][RegularExpression(_deviceIdRegexPattern)] string id)
        {
            try
            {
                return Results.Ok(await _mediator.Send(new GetDeviceHistoryQuery(id)));
            }
            catch (Exception)
            {
                return Results.NotFound();
            }
        }
    }
}
