using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Generated;
using System.Net.Http;
using Workflow = SOC.Conductor.Entities.Workflow;

namespace SOC.Conductor.Services;

public class IoTTriggerEvaluationService : BackgroundService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkflowResourceClient _workflowResourceClient;

    // TODO: Inject queue this service will subscribe to
    public IoTTriggerEvaluationService(
        IUnitOfWork unitOfWork,
        IWorkflowResourceClient workflowResourceClient
    )
    {
        _unitOfWork = unitOfWork;
        _workflowResourceClient = workflowResourceClient;
    }

    protected override async System.Threading.Tasks.Task ExecuteAsync(
        CancellationToken stoppingToken
    )
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Retrieve all IoT triggers from the database
            var iotTriggers = await _unitOfWork.IoTTriggers.GetAllAsync();

            foreach (var iotTrigger in iotTriggers)
            {
                // Evaluate the IoT trigger
                bool evaluationResult = EvaluateIoTTrigger(
                    iotTrigger,
                    "actual_value_from_iot_device"
                ); // TODO: pass actual value somehow

                if (evaluationResult)
                {
                    // Retrieve workflows associated with the IoT trigger
                    var workflows = await _unitOfWork.Automations.GetWorkflowsByTriggerIdAsync(
                        iotTrigger.Id
                    );

                    foreach (var workflow in workflows)
                    {
                        await ProcessWorkflow(workflow);
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
        return true;
        // Check if the actual property value matches the trigger value based on the specified condition
        /*if (ioTTrigger.Condition.Equals("Equals", StringComparison.OrdinalIgnoreCase))
        {
            return actualValue.Equals(ioTTrigger.Value, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            throw new NotSupportedException(
                $"Condition '{ioTTrigger.Condition}' is not supported."
            );
        }*/
        // TODO: Add checks for all operators, use switch for more elegant code
    }

    private async System.Threading.Tasks.Task ProcessWorkflow(Workflow workflow)
    {
        // TODO: generate body
        await _workflowResourceClient.StartWorkflowAsync(
            workflow.Name,
            new Dictionary<string, object>(),
            1
        );
    }
}
