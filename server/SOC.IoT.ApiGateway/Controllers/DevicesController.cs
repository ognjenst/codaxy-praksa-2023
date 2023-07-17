using Microsoft.AspNetCore.Mvc;
using SOC.IoT.ApiGateway.Controllers.Examples;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.Base.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace SOC.IoT.ApiGateway.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceManager _deviceManager;
    private const string _deviceIdRegexPattern = "0[xX][0-9a-fA-F]+";

    public DevicesController(IDeviceManager deviceManager)
    {
        _deviceManager = deviceManager;
    }

    /// <summary>
    /// Returns a list of all registered devices with their capabilities and last states
    /// </summary>
    /// <returns>Returns a list of all registered devices with their capabilities and last states</returns>
    [HttpGet(Name = "GetDevices")]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DevicesExample))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DeviceDTO>))]
    public IEnumerable<DeviceDTO> GetDevices()
    {
        return _deviceManager.GetDevices().Select(d => new DeviceDTO(d));
    }

    /// <summary>
    /// Returns a single device with its last known state
    /// </summary>
    /// <param name="id">ID of the device</param>
    /// <returns>Single device with its last known state</returns>
    [HttpGet("{id}", Name = "GetDevice")]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeviceExample))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
    public IResult GetDevice([FromRoute] [RegularExpression(_deviceIdRegexPattern)] string id)
    {
        try
        {
            return Results.Ok(new DeviceDTO(_deviceManager.GetDevice(id)));
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
            await _deviceManager.SetDeviceStateAsync(payload.GetDevice(id));
            return Results.NoContent();
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
    }
}
