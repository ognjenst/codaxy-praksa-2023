using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Handler; 


public class DeviceRequest : IRequest<DeviceResponse> {
}

public class DeviceResponse {

}

[OriginalName("SCANNING_for_device_tasks")]
internal class DeviceHandler : ITaskRequestHandler<DeviceRequest, DeviceResponse> {

    private readonly ILogger<DeviceHandler> _logger;

    public DeviceHandler(ILogger<DeviceHandler> logger) {
        _logger = logger;
    }

    public Task<DeviceResponse> Handle(DeviceRequest request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}
