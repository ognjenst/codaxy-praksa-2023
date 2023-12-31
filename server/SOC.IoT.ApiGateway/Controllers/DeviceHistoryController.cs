﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOC.IoT.ApiGateway.Controllers.Examples;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.ApiGateway.Models;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;

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
        /// Returns a list of device states for device with given ID
        /// </summary>
        /// <param name="id">ID of the device</param>
        /// <returns>List of states of the device with given ID</returns>
        [Authorize("Read-DeviceHistory")]
        [HttpGet("{id}", Name = "GetDeviceHistory")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeviceHistoryExample))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceHistoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<IResult> GetDeviceHistory([FromRoute][RegularExpression(_deviceIdRegexPattern)] string id)
        {
            var dto = await _mediator.Send(new GetDeviceHistoryQuery(id));
            if (dto is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(dto);
        }
    }
}
