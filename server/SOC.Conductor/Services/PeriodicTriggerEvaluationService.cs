using MediatR;
using Microsoft.EntityFrameworkCore;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;
using SOC.Conductor.Generated;
using SOC.Conductor.Models;
using System.Diagnostics;

namespace SOC.Conductor.Services;

using Task = System.Threading.Tasks.Task;

public class PeriodicTriggerEvaluationService : BackgroundService, INotificationHandler<TriggerNotification>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkflowResourceClient _workflowResourceClient;
    private readonly ILogger<PeriodicTriggerEvaluationService> _logger;
    private readonly Dictionary<int, Timer> _triggerEvaluators = new();
    public PeriodicTriggerEvaluationService(IUnitOfWork unitOfWork, IWorkflowResourceClient workflowResourceClient, ILogger<PeriodicTriggerEvaluationService> logger)
    {
        _unitOfWork = unitOfWork;
        _workflowResourceClient = workflowResourceClient;
        _logger = logger;
    }

    public Task Handle(TriggerNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Trigger is not PeriodicTrigger) return Task.CompletedTask;
        var task = notification.Trigger as PeriodicTrigger;

        if (notification.Deleted)
            _triggerEvaluators.Remove(task.Id);
        else
            _triggerEvaluators.Add(task.Id, Evaluate(task));

        return Task.CompletedTask;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var periodicTriggers = await _unitOfWork.PeriodicTriggers.GetAllAsync();

        periodicTriggers.ForEach(periodicTrigger => _triggerEvaluators.Add(periodicTrigger.Id, Evaluate(periodicTrigger)));
    }

    private Timer Evaluate(PeriodicTrigger trigger)
    {
        var now = DateTime.UtcNow.TimeOfDay;
        var delay = trigger.Start >= now ? trigger.Start.Subtract(now) : now.Subtract(trigger.Start);

        int minutes = trigger.Period;

        switch (trigger.Unit)
        {
            case Entities.Enums.PeriodTriggerUnit.HOURS: minutes *= 60; break;
            case Entities.Enums.PeriodTriggerUnit.DAYS: minutes *= 60 * 24; break;
        }

        return new Timer(async (_) =>
        {
            var workflows = (await _unitOfWork.Workflows.GetAllAsync()).Where(w => w.Triggers.Any(t => t.Id == trigger.Id)).ToList();

            workflows.ForEach(async (workflow) =>
            {
                try
                {
                    await _workflowResourceClient.StartWorkflow_1Async(new StartWorkflowRequest()
                    {
                        Name = workflow.Name,
                        Version = workflow.Version
                    });
                }
                catch (ApiException ex)
                {
                    _logger.LogError($"Workflow start failed. {ex.Message}");
                }
            });
        }, null, delay, TimeSpan.FromMinutes(minutes));
    }
}
