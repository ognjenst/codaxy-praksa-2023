using AutoMapper;
using Microsoft.Extensions.Options;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;
using SOC.IoT.Handler;
using SOC.IoT.Options;

namespace SOC.IoT.Services;

public interface IDeviceService
{
    Task LigthBulbInRepetitions(DetectionRequest request, CancellationToken cancellationToken);
}

public class DeviceService : IDeviceService
{
    private readonly DeviceOptions _options;
    private readonly IMapper _mapper;
    private readonly IDeviceManager _deviceManager;

    public DeviceService(
        IOptions<DeviceOptions> options,
        IMapper mapper,
        IDeviceManager deviceManager
    )
    {
        _options = options.Value;
        _mapper = mapper;
        _deviceManager = deviceManager;
    }

    public async Task LigthBulbInRepetitions(
        DetectionRequest request,
        CancellationToken cancellationToken
    )
    {
        var device = _deviceManager.GetDevice(request.DeviceId);

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

                var updateDeviceDto = _mapper.Map<Device>(updateDeviceObj);

                await _deviceManager.SetDeviceStateAsync(updateDeviceDto);

                await Task.Delay(_options.DelayTime, cancellationToken);

                updateDeviceDto.State!.State = false;

                await _deviceManager.SetDeviceStateAsync(updateDeviceDto);

                await Task.Delay(_options.DelayTime, cancellationToken);
            }
        }
    }
}
