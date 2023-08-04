using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SOC.Conductor.Contracts;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;
using SOC.Conductor.Entities.Enums;
using SOC.Conductor.Generated;
using SOC.Conductor.Models;
using SOC.Conductor.Models.Enums;
using System.Diagnostics;

namespace SOC.Conductor.Services;

using Task = System.Threading.Tasks.Task;

public class PeriodicTriggerEvaluationService : BackgroundService, INotificationHandler<TriggerNotification>
{
    private readonly ILogger<PeriodicTriggerEvaluationService> _logger;
    private static readonly Dictionary<int, Timer> _triggerEvaluators = new();
    private readonly IServiceScopeFactory _services;
    public PeriodicTriggerEvaluationService(IServiceScopeFactory services, ILogger<PeriodicTriggerEvaluationService> logger)
    {
        _services = services;
        _logger = logger;
    }

    public Task Handle(TriggerNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Trigger is not PeriodicTrigger) return Task.CompletedTask;
        var task = notification.Trigger as PeriodicTrigger;

        if (notification.Action == TriggerNotificationAction.DELETE && _triggerEvaluators.ContainsKey(task.Id))
        {
             _triggerEvaluators[task.Id].Change(Timeout.Infinite, Timeout.Infinite);
            _triggerEvaluators.Remove(task.Id);
        }
        else if (notification.Action == TriggerNotificationAction.CREATE && !_triggerEvaluators.ContainsKey(task.Id))
            _triggerEvaluators.Add(task.Id, Evaluate(task));
        else if (notification.Action == TriggerNotificationAction.UPDATE && _triggerEvaluators.ContainsKey(task.Id))
        {
            var (delay, minutes) = GetTimerParameters(task);
            _triggerEvaluators[task.Id].Change(delay, minutes);
        }

        return Task.CompletedTask;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();

        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var periodicTriggers = await _unitOfWork.PeriodicTriggers.GetAllAsync();

        periodicTriggers.ForEach(periodicTrigger => _triggerEvaluators.Add(periodicTrigger.Id, Evaluate(periodicTrigger)));
    }

    private (TimeSpan, TimeSpan) GetTimerParameters(PeriodicTrigger trigger)
    {
        int minutes = trigger.Period;

        switch (trigger.Unit)
        {
            case PeriodTriggerUnit.HOURS: minutes *= 60; break;
            case PeriodTriggerUnit.DAYS: minutes *= 60 * 24; break;
        }

        var now = DateTime.UtcNow;
        var delay = trigger.Start >= now ? trigger.Start.Subtract(now) : TimeSpan.FromSeconds(minutes * 60 - ((now - trigger.Start).TotalSeconds % (minutes * 60)));

        return (delay, TimeSpan.FromMinutes(minutes));
    }

    private Timer Evaluate(PeriodicTrigger trigger)
    {
        var (delay, minutes) = GetTimerParameters(trigger);

        return new Timer(async (_) =>
        {
            using var scope = _services.CreateScope();

            var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var _workflowResourceClient = scope.ServiceProvider.GetRequiredService<IWorkflowResourceClient>();

            var workflows = await _unitOfWork.Automations.GetWorkflowsByTriggerIdAsync(trigger.Id);

            workflows.ForEach(async (workflow) =>
            {
                using var scope = _services.CreateScope();

                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var _workflowResourceClient = scope.ServiceProvider.GetRequiredService<IWorkflowResourceClient>();

                var automation = (await _unitOfWork.Automations.GetByCondition(a => a.WorkflowId == workflow.Id && a.TriggerId == trigger.Id)).FirstOrDefault();
                if (automation is null) return;
                try
                {
                    await _workflowResourceClient.StartWorkflow_1Async(new StartWorkflowRequest()
                    {
                        Name = workflow.Name,
                        Version = workflow.Version,
                        Input = automation.InputParameters.ToObject<Dictionary<string, object>>()
                    });
                }
                catch (ApiException ex)
                {
                    _logger.LogError($"Workflow start failed. {ex.Message}");
                }
            });
        }, null, delay, minutes);
    }
}
