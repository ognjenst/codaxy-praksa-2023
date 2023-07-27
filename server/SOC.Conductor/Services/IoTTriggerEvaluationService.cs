using Newtonsoft.Json.Linq;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Enums;
using SOC.Conductor.Generated;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Domain.Entity;
using SOC.IoT.Domain.Enum;
using Task = System.Threading.Tasks.Task;
using Workflow = SOC.Conductor.Entities.Workflow;

namespace SOC.Conductor.Services;

public class IoTTriggerEvaluationService : BackgroundService
{
    private const int VERSION_NUMBER = 1;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<IoTTriggerEvaluationService> _logger;
    private readonly Dictionary<string, Guid> _listeners = new();
    private List<string> deviceIds = new List<string>() { "0x00124b0022d2d320", "0x00124b00226969ac" };

    public IoTTriggerEvaluationService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<IoTTriggerEvaluationService> logger
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var deviceManager = GetDeviceManager();

        foreach (var deviceId in deviceIds)
        {
            var listenerId = deviceManager.AttachListener(deviceId, ProcessDeviceUpdate);
            _listeners.Add(deviceId, listenerId);
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500, cancellationToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        var deviceManager = GetDeviceManager();

        foreach (var (deviceId, listenerId) in _listeners)
            deviceManager.DetachListener(listenerId);

        await base.StopAsync(cancellationToken);
    }

    private IDeviceManager GetDeviceManager()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<IDeviceManager>();
    }

    private async Task ProcessDeviceUpdate(Device device)
    {
        _logger.LogInformation("Device {@Device}", device);
        if (device.Capabilities.Contains(DeviceCapability.Temperature) && device?.Temperature is null) return;
        if (device.Capabilities.Contains(DeviceCapability.Contact) && device?.Contact is null) return;

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var iotTriggers = await unitOfWork.IoTTriggers.GetAllAsync();

            foreach (var iotTrigger in iotTriggers.Where(e => e.DeviceId.Equals(device.Id)))
            {
                string actualValue = GetDeviceValue(device, iotTrigger.Property);
                if (string.IsNullOrEmpty(actualValue))
                    continue;
                if (EvaluateIoTTrigger(iotTrigger, actualValue))
                {
                    var workflows = await unitOfWork.IoTTriggers.GetWorkflowsByTriggerIdAsync(iotTrigger.Id);
                    foreach (var workflow in workflows)
                    {
                        var automation = (await unitOfWork.Automations.GetByCondition((a) => a.WorkflowId == workflow.Id && a.TriggerId == iotTrigger.Id)).FirstOrDefault();
                        if (automation != null)
                        {
                            await ProcessWorkflow(workflow, UpdateWorkflowInputParameters(automation.InputParameters, device, iotTrigger));
                        }
                    }
                }
            }
        }
    }

    private JObject UpdateWorkflowInputParameters(JObject inputParameters, Device device, IoTTrigger trigger)
    {
        var propertyInfo = typeof(Device).GetProperty(trigger.Property);
        if (propertyInfo != null)
        {
            var propertyValue = propertyInfo.GetValue(device);
            string? actualValue;
            actualValue = trigger.Property switch
            {
                nameof(DeviceProperty.Temperature) => ((DeviceTemperature)propertyValue).Value.ToString(),
                nameof(DeviceProperty.Humidity) => ((DeviceHumidity)propertyValue).Value.ToString(),
                nameof(DeviceProperty.Contact) => ((DeviceContact)propertyValue).Value.ToString(),
                _ => throw new NotSupportedException($"Property '{trigger.Property}' is not supported.")
            };

            string customMessage = $"Device {device.Id} has changed {trigger.Property}.\nActualValue: {actualValue} {trigger.Condition} TriggerValue: {trigger.Value}";
            JObject messageObject = new JObject { { "message", customMessage } };

            if (inputParameters is null)
                throw new ArgumentNullException(nameof(inputParameters), "Input parameters are not set.");
            else
            {
                // Merge the messageObject with the inputParameters
                foreach (var property in messageObject.Properties())
                {
                    inputParameters[property.Name] = property.Value;
                }
            }
        }
        return inputParameters;
    }

    private string GetDeviceValue(Device device, string property)
    {
        string actualValue = "";

        if (property is null || device is null)
            return actualValue;

        actualValue = property switch
        {
            nameof(DeviceProperty.Temperature) when device?.Temperature is not null => device.Temperature.Value.ToString(),
            nameof(DeviceProperty.Humidity) when device?.Humidity is not null => device.Humidity.Value.ToString(),
            nameof(DeviceProperty.Contact) when device?.Contact is not null => device.Contact.Value.ToString(),
            _ => actualValue
        };

        return actualValue;
    }

    private bool EvaluateIoTTrigger(IoTTrigger ioTTrigger, string actualValue)
    {
        if (ioTTrigger == null)
        {
            throw new ArgumentNullException(nameof(ioTTrigger), "Trigger cannot be null.");
        }

        if (string.IsNullOrEmpty(ioTTrigger.Property) || string.IsNullOrEmpty(ioTTrigger.Value))
        {
            throw new ArgumentException("Trigger property and value must be specified.");
        }

        // For non-numeric values, evaluation can be done only for EQuality
        if (ioTTrigger.Condition.Equals(Entities.Enums.Operator.EQ))
            return actualValue.Equals(ioTTrigger.Value, StringComparison.OrdinalIgnoreCase);

        // For numeric values
        if (double.TryParse(actualValue, out double actualValueDouble) && double.TryParse(ioTTrigger.Value, out double iotTriggerValueDouble))
        {
            // Check if the actual property value matches the trigger value based on the specified condition
            return ioTTrigger.Condition switch
            {
                Operator.EQ => actualValue.Equals(ioTTrigger.Value, StringComparison.OrdinalIgnoreCase),
                Operator.GT => actualValueDouble > iotTriggerValueDouble,
                Operator.LT => actualValueDouble < iotTriggerValueDouble,
                Operator.GEQ => actualValueDouble >= iotTriggerValueDouble,
                Operator.LEQ => actualValueDouble <= iotTriggerValueDouble,
                _ => throw new NotSupportedException($"Condition '{ioTTrigger.Condition}' is not supported.")
            };
        }
        else
            throw new Exception("Parsing actualValue or iotTrigger.Value as a double failed.");
    }

    private async Task ProcessWorkflow(Workflow workflow, JObject inputParameters)
    {
        var body = GenerateBody(inputParameters);
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            IWorkflowResourceClient _workflowResourceClient = scope.ServiceProvider.GetRequiredService<IWorkflowResourceClient>();
            await _workflowResourceClient.StartWorkflowAsync(
                workflow.Name,
                body,
                VERSION_NUMBER
            );
        }
    }

    private Dictionary<string, object> GenerateBody(JObject inputParameters)
    {
        if (inputParameters == null)
            return new Dictionary<string, object>();
        return inputParameters.ToObject<Dictionary<string, object>>();
    }
}
