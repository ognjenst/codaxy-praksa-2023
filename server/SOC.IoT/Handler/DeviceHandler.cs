using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using SOC.IoT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Handler; 


public class DeviceRequest : IRequest<NoOutput> {
    string id;
}

[OriginalName("device_task")]
public class DeviceHandler : ITaskRequestHandler<DeviceRequest, NoOutput> {

    private readonly IDeviceService _service;

    public DeviceHandler(IDeviceService service) {
        _service = service;
    }

    public async Task<NoOutput> Handle(DeviceRequest request, CancellationToken cancellationToken) {
        //here i should call device service
        //so that it call apigateway and changes the device properties

        //i should pass deviceId and DeviceUpdateDTO(lookup in apiGateway)
        
        //when this method is called then the polling has been done
        //but the question remains when will it call update?
        await _service.UpdateDevice("");

        throw new NotImplementedException();
    }
}
