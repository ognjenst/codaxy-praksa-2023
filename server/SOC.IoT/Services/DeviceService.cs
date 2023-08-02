using AutoMapper;
using Microsoft.Extensions.Options;
using SOC.IoT.Generated;
using SOC.IoT.Handler;
using SOC.IoT.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOC.IoT.Services;

public interface IDeviceService
{
	Task LigthBulbInRepetitions(DetectionRequest request, CancellationToken cancellationToken); 
}

public class DeviceService : IDeviceService
{
	private readonly IDevicesClient _devicesClient;
	private readonly DeviceOptions _options;
	private readonly IMapper _mapper;

    public DeviceService(IDevicesClient devicesClient, IOptions<DeviceOptions> options, IMapper mapper)
    {
        _devicesClient = devicesClient;
		_options = options.Value;
		_mapper = mapper;
    }
    public async Task LigthBulbInRepetitions(DetectionRequest request, CancellationToken cancellationToken)
	{
		var device = await _devicesClient.GetDeviceAsync(request.DeviceId, cancellationToken);

		if (device != null)
		{
			int numberOfRepetitions = 0;

			var updateDeviceObj = new DeviceRequest
			{
				Id = device.Id,
				Brightness = _options.Brightness,
				X = request.X,
				Y = request.Y,
				State = true
			};

			while (numberOfRepetitions <= request.MaxNumberOfRepetitions)
			{
				numberOfRepetitions++;

				var updateDeviceDto = _mapper.Map<DeviceUpdateDTO>(updateDeviceObj);

				// turn on light and delay for few seconds
				await _devicesClient.UpdateDeviceAsync(device.Id, updateDeviceDto, cancellationToken);
				await Task.Delay(_options.DelayTime);

				updateDeviceDto.State.State = false;

				// turn off light and delay for few seconds 
				await _devicesClient.UpdateDeviceAsync(device.Id, updateDeviceDto, cancellationToken);

				await Task.Delay(_options.DelayTime);
			}
		}
		
	}
}
