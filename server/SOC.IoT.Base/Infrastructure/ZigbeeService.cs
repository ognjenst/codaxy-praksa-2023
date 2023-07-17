using Microsoft.Extensions.Logging;
using MQTTnet;
using Newtonsoft.Json.Linq;
using SOC.IoT.Base.Infrastructure.Zigbee;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;

namespace SOC.IoT.Base.Infrastructure;

internal class ZigbeeService : IZigbeeService
{
    private readonly ILogger<ZigbeeService> _logger;
    private readonly IMqttService _mqttService;
    private readonly IDeviceDescriptionManager _deviceDescriptionManager;
    private readonly IDeviceManager _deviceManager;
    private readonly IDeviceWriter _deviceWriter;

    private const string Z2M_DEVICES_TOPIC = "zigbee2mqtt/bridge/devices";
    private bool _readCompleted = false;
    private static object _lock = new();

    public ZigbeeService(
        ILogger<ZigbeeService> logger,
        IMqttService mqttService,
        IDeviceDescriptionManager deviceDescriptionManager,
        IDeviceManager deviceManager,
        IDeviceWriter deviceWriter
    )
    {
        _logger = logger;
        _mqttService = mqttService;
        _deviceDescriptionManager = deviceDescriptionManager;
        _deviceManager = deviceManager;
        _deviceWriter = deviceWriter;
    }

    public async Task DiscoverDevicesAsync()
    {
        await _mqttService.SubscribeAsync(
            Z2M_DEVICES_TOPIC,
            (msg) =>
            {
                lock (_lock)
                {
                    if (_readCompleted)
                        return Task.CompletedTask;

                    var message = msg.ApplicationMessage.ConvertPayloadToString();
                    if (message is null)
                        return Task.CompletedTask;

                    var devices = JArray.Parse(message);
                    if (devices is null)
                        return Task.CompletedTask;

                    foreach (var deviceDescription in devices.AsEnumerable())
                    {
                        if (deviceDescription is null)
                            continue;

                        var physicalAddress = deviceDescription["ieee_address"]?.ToString();
                        if (physicalAddress is null)
                            continue;

                        var device = deviceDescription.ToObject<JObject>();
                        if (device is null)
                            continue;

                        var description = new DeviceDescription(device);

                        _deviceDescriptionManager.RegisterPayload(physicalAddress, description);
                        _deviceManager.UpdateDeviceState(
                            new Device
                            {
                                Id = physicalAddress,
                                Capabilities = description.Capabilities
                            }
                        );

                        _mqttService.SubscribeAsync(
                            $"zigbee2mqtt/{physicalAddress}",
                            (msg) =>
                            {
                                var message = msg.ApplicationMessage.ConvertPayloadToString();
                                if (message is null)
                                    return Task.CompletedTask;

                                var payload = JObject.Parse(message);
                                if (payload is null)
                                    return Task.CompletedTask;

                                var device = _deviceWriter.ReadDevice(description, payload);
                                _deviceManager.UpdateDeviceState(device);
                                return Task.CompletedTask;
                            }
                        );
                    }

                    _readCompleted = true;
                }
                return Task.CompletedTask;
            }
        );

        _logger.LogInformation("Discovering Zigbee devices.");
    }
}
