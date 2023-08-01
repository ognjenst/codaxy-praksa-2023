using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOC.IoT.ApiGateway.Controllers.Examples;
using SOC.IoT.ApiGateway.Handlers;
using SOC.IoT.ApiGateway.Helpers;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.Base.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace SOC.IoT.ApiGateway.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DevicesController : ControllerBase
{
    private readonly IMediator _mediator;

    private const string _deviceIdRegexPattern = "0[xX][0-9a-fA-F]+";

    public DevicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

	/// <summary>
	/// Returns a list of all registered devices with their capabilities and last states
	/// </summary>
	/// <returns>Returns a list of all registered devices with their capabilities and last states</returns>
	[PermissionAuthorize("Read-Device")]
	[HttpGet(Name = "GetDevices")]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DevicesExample))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DeviceDTO>))]
    public async Task<IEnumerable<DeviceDTO>> GetDevices()
    {
        return await _mediator.Send(new GetDevicesQuery());
    }

	/// <summary>
	/// Returns a single device with its last known state
	/// </summary>
	/// <param name="id">ID of the device</param>
	/// <returns>Single device with its last known state</returns>
	[PermissionAuthorize("Read-Device")]
	[HttpGet("{id}", Name = "GetDevice")]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeviceExample))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
    public async Task<IResult> GetDevice([FromRoute] [RegularExpression(_deviceIdRegexPattern)] string id)
    {
        try
        {
            DeviceDTO deviceDTO = await _mediator.Send(new GetDeviceQuery(id));
            if (deviceDTO is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(deviceDTO);
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
    }

    /// <summary>
    /// Sends a command to update a device's state
    /// </summary>
    /// <param name="id">ID of the device</param>
    /// <param name="payload">State that will be applied to the device</param>
    /// <returns></returns>
    [PermissionAuthorize("Update-Device")]
    [HttpPut("{id}", Name = "UpdateDevice")]
    [SwaggerRequestExample(typeof(DeviceUpdateDTO), typeof(UpdateDeviceExample))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
    public async Task<IResult> UpdateDevice(
        [FromRoute] [RegularExpression(_deviceIdRegexPattern)] string id,
        [FromBody] DeviceUpdateDTO payload
    )
    {
        try
        {
            await _mediator.Send(new UpdateDeviceStateQuery(id, payload));
            return Results.NoContent();
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
    }
}
