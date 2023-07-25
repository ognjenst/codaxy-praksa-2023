using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Generated;
using SOC.Conductor.Repositories;
using System;
using System.Net.Http;
using Workflow = SOC.Conductor.Entities.Workflow;

namespace SOC.Conductor.Services;

public class IoTTriggerEvaluationService : BackgroundService
{
    private const int VERSION_NUMBER = 1;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    // TODO: Inject queue this service will subscribe to
    public IoTTriggerEvaluationService(
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                IUnitOfWork _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
                IWorkflowResourceClient _workflowResourceClient = serviceProvider.GetRequiredService<IWorkflowResourceClient>();

                // Retrieve all IoT triggers from the database
                var iotTriggers = await _unitOfWork.IoTTriggers.GetAllAsync();

                foreach (var iotTrigger in iotTriggers)
                {
                    bool evaluationResult = EvaluateIoTTrigger(
                        iotTrigger,
                        "actual_value_from_iot_device"
                    ); // TODO: pass actual value somehow

                    if (evaluationResult)
                    {
                        // Retrieve workflows associated with the IoT trigger
                        var workflows = await _unitOfWork.Automations.GetWorkflowsByTriggerIdAsync(iotTrigger.Id);

                        var automation = (await _unitOfWork.Automations.GetAllAsync()).FirstOrDefault();

                        if (automation != null)
                        {
                            foreach (var workflow in workflows)
                            {
                                await ProcessWorkflow(workflow, automation.InputParameters);
                            }
                        }
                    }
                }
            }
            // Delay the execution for some time before checking again
            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
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
        return inputParameters.ToObject<Dictionary<string, object>>();
    }
}
