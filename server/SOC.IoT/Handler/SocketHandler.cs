using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOC.IoT.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Handler
{
    public class SocketRequest : IRequest<NoOutput>
    {
        [JsonProperty("deviceId")]
        public string? DeviceId { get; set; }
    }

    [OriginalName("IoT_socket_state_change")]
    public class SocketHandler : ITaskRequestHandler<SocketRequest, NoOutput>
    {
        private readonly IDevicesClient _devicesClient;
        private readonly ILogger<SocketHandler> _logger;

        public SocketHandler(IDevicesClient devicesClient, ILogger<SocketHandler> logger)
        {
            _devicesClient = devicesClient;
            _logger = logger;
        }

        public async Task<NoOutput> Handle(
            SocketRequest request,
            CancellationToken cancellationToken
        )
        {
            var device = await _devicesClient.GetDeviceAsync(request.DeviceId, cancellationToken);
            var state = device.State.State;

            if (state == false)
            {
                var deviceUpdate = new DeviceUpdateDTO()
                {
                    State = new DeviceState() { State = true }
                };
                try
                {
                    await _devicesClient.UpdateDeviceAsync(
                        request.DeviceId,
                        deviceUpdate,
                        cancellationToken
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        $"Error while changing state of device {request.DeviceId}. {ex.Message}"
                    );
                }
            }
            return new NoOutput();
        }
    }
}
