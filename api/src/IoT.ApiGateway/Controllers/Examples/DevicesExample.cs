﻿using IoT.ApiGateway.Models;
using IoT.Domain.Entity;
using IoT.Domain.Enum;
using Swashbuckle.AspNetCore.Filters;

namespace IoT.ApiGateway.Controllers.Examples;

internal class DevicesExample : IExamplesProvider<IEnumerable<DeviceDTO>>
{
    public IEnumerable<DeviceDTO> GetExamples() =>
        new[]
        {
            new DeviceDTO(
                new Device
                {
                    Id = "0x0123456789abcdef",
                    Capabilities = new[]
                    {
                        DeviceCapability.Temperature,
                        DeviceCapability.Humidity,
                        DeviceCapability.Battery
                    }
                }
            ),
            new DeviceDTO(
                new Device
                {
                    Id = "0xfedcba9876543210",
                    Capabilities = new[]
                    {
                        DeviceCapability.State,
                        DeviceCapability.Light,
                        DeviceCapability.ColorXy
                    },
                    State = new DeviceState { State = true }
                }
            )
        };
}
