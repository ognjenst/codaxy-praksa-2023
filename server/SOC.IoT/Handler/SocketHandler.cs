using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;
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

    [OriginalName("IoT_socket_turn_on")]
    public class SocketHandler : ITaskRequestHandler<SocketRequest, NoOutput>
    {
        private readonly IDeviceManager _deviceManager;
        private readonly ILogger<SocketHandler> _logger;

        public SocketHandler(IDeviceManager deviceManager, ILogger<SocketHandler> logger)
        {
            _deviceManager = deviceManager;
            _logger = logger;
        }

        public async Task<NoOutput> Handle(
            SocketRequest request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("Socket changing state started.");
            var device = _deviceManager.GetDevice(request.DeviceId);
            var state = device.State!.State;

            if (state.HasValue && !state.Value)
            {
                device.State = new DeviceState() { State = true };
                try
                {
                    await _deviceManager.SetDeviceStateAsync(device);
                    _logger.LogInformation("Socket turned on.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        $"Error while changing state of device {request.DeviceId}. {ex.Message}"
                    );
                }
            }
            else
            {
                _logger.LogInformation("Socket is already turned on.");
            }
            return new NoOutput();
        }
    }
}
