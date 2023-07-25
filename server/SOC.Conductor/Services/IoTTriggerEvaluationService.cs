using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Generated;
using SOC.Conductor.Repositories;
using SOC.IoT.Base.Interfaces;
using SOC.IoT.Base.Services;
using System;
using System.Net.Http;
using Workflow = SOC.Conductor.Entities.Workflow;

namespace SOC.Conductor.Services;

public class IoTTriggerEvaluationService : BackgroundService
{
    private const string deviceId = "0x00124b0022d2d320"; // TODO: HashMap for different types of sensors, type - deviceId
    private const int VERSION_NUMBER = 1;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public IoTTriggerEvaluationService(
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                var _workflowResourceClient = serviceProvider.GetRequiredService<IWorkflowResourceClient>();
                var deviceManager = serviceProvider.GetRequiredService<IDeviceManager>();

                // Retrieve all IoT triggers from the database
                var iotTriggers = await _unitOfWork.IoTTriggers.GetAllAsync();

                foreach (var iotTrigger in iotTriggers)
                {
                    await foreach (var device in deviceManager.SubscribeAsync(deviceId, cancellationToken))
                    {
                        bool evaluationResult = EvaluateIoTTrigger(
                        iotTrigger,
                        device.Temperature.Value.ToString() // TODO: Generilize for different types of sensors
                        ); 

                        if (evaluationResult)
                        {
                            // Retrieve workflows associated with the IoT trigger
                            var workflows = await _unitOfWork.Automations.GetWorkflowsByTriggerIdAsync(iotTrigger.Id);
                            foreach (var workflow in workflows)
                            {
                                var automation = await _unitOfWork.Automations.GetById(iotTrigger.Id, workflow.Id); 
                                if (automation != null)
                                {
                                    await ProcessWorkflow(workflow, automation.InputParameters);
                                }
                            }
                        }
                    }
                }
            }
            // Delay the execution for some time before checking again
            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
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

        if (double.TryParse(actualValue, out double actualValueDouble) && double.TryParse(ioTTrigger.Value, out double iotTriggerValueDouble))
        {
            // Check if the actual property value matches the trigger value based on the specified condition
            switch (ioTTrigger.Condition)
            {
                case Entities.Enums.Operator.EQ:
                    return actualValue.Equals(ioTTrigger.Value, StringComparison.OrdinalIgnoreCase);
                case Entities.Enums.Operator.GT:
                    return actualValueDouble > iotTriggerValueDouble;
                case Entities.Enums.Operator.LT:
                    return actualValueDouble < iotTriggerValueDouble;
                case Entities.Enums.Operator.GEQ:
                    return actualValueDouble >= iotTriggerValueDouble;
                case Entities.Enums.Operator.LEQ:
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

    private async System.Threading.Tasks.Task ProcessWorkflow(Workflow workflow, JObject inputParameters)
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
