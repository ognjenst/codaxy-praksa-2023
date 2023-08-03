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
    private readonly IDeviceManager _deviceManager;

    public DeviceService(IOptions<DeviceOptions> options, IDeviceManager deviceManager)
    {
        _options = options.Value;
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

            device.Light = new DeviceLight { Brightness = ((decimal)_options.Brightness) };
            device.ColorXy = new DeviceColorXy { X = (decimal)request.X, Y = (decimal)request.Y };
            device.State = new DeviceState { State = true };

            while (numberOfRepetitions <= request.MaxNumberOfRepetitions)
            {
                numberOfRepetitions++;

                await _deviceManager.SetDeviceStateAsync(device);

                await Task.Delay(_options.DelayTime, cancellationToken);

                device.State!.State = false;

                await _deviceManager.SetDeviceStateAsync(device);

                await Task.Delay(_options.DelayTime, cancellationToken);

                device.State!.State = true;
            }
        }
    }
}
