using Microsoft.EntityFrameworkCore.Metadata;
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
        using var scope = _serviceScopeFactory.CreateScope();
        var deviceManager = scope.ServiceProvider.GetRequiredService<IDeviceManager>();

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
                string actualValue = GetDeviceValue(device, iotTrigger.Property); // TODO
                if (string.IsNullOrEmpty(actualValue))
                    continue;
                if (EvaluateIoTTrigger(iotTrigger, actualValue))
                {
                    var workflows = await unitOfWork.Automations.GetWorkflowsByTriggerIdAsync(iotTrigger.Id);
                    foreach (var workflow in workflows)
                    {
                        var automation = await unitOfWork.Automations.GetById(iotTrigger.Id, workflow.Id);
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
            switch (trigger.Property)
            {
                case nameof(DeviceProperty.Temperature):
                    actualValue = ((DeviceTemperature)propertyValue).Value.ToString();
                    break;
                case nameof(DeviceProperty.Humidity):
                    actualValue = ((DeviceHumidity)propertyValue).Value.ToString();
                    break;
                case nameof(DeviceProperty.Contact):
                    actualValue = ((DeviceContact)propertyValue).Value.ToString();
                    break;
                default:
                    throw new NotSupportedException($"Property '{trigger.Property}' is not supported.");
            }

            string customMessage = $"Device {device.Id} has changed {trigger.Property}.\nActualValue: {actualValue} {trigger.Condition} TriggerValue: {trigger.Value}";
            if (inputParameters is null)
                inputParameters = new JObject { { "message", customMessage } };
            else
                inputParameters["message"] = customMessage;
        }
        return inputParameters;
    }

    private static string GetDeviceValue(Device device, string property)
    {
        string actualValue = "";
        if (nameof(DeviceProperty.Temperature).Equals(property) && device?.Temperature is not null)
        {
            actualValue = device.Temperature.Value.ToString();
        }
        else if (nameof(DeviceProperty.Humidity).Equals(property) && device?.Humidity is not null)
        {
            actualValue = device.Humidity.Value.ToString();
        }
        else if (nameof(DeviceProperty.Contact).ToString().Equals(property) && device?.Contact is not null)
        {
            actualValue = device.Contact.Value.ToString();
        }

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
            switch (ioTTrigger.Condition)
            {
                case Operator.EQ:
                    return actualValue.Equals(ioTTrigger.Value, StringComparison.OrdinalIgnoreCase);
                case Operator.GT:
                    return actualValueDouble > iotTriggerValueDouble;
                case Operator.LT:
                    return actualValueDouble < iotTriggerValueDouble;
                case Operator.GEQ:
                    return actualValueDouble >= iotTriggerValueDouble;
                case Operator.LEQ:
                    return actualValueDouble <= iotTriggerValueDouble;
                default:
                    throw new NotSupportedException(
                    $"Condition '{ioTTrigger.Condition}' is not supported."
                );
            }
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
