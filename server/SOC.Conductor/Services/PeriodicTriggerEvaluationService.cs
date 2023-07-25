using Microsoft.EntityFrameworkCore;
using SOC.Conductor.Entities;
using SOC.Conductor.Entities.Contexts;
using SOC.Conductor.Generated;
using System.Diagnostics;

namespace SOC.Conductor.Services;

using Task = System.Threading.Tasks.Task;

public class PeriodicTriggerEvaluationService : BackgroundService
{
    private readonly SOCDbContext _context;
    private readonly IWorkflowResourceClient _workflowResourceClient;
    private readonly ILogger<PeriodicTriggerEvaluationService> _logger;
    private readonly Dictionary<int, Timer> _triggerEvaluators = new();
    public PeriodicTriggerEvaluationService(SOCDbContext context, IWorkflowResourceClient workflowResourceClient, ILogger<PeriodicTriggerEvaluationService> logger)
    {
        _context = context;
        _workflowResourceClient = workflowResourceClient;
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var periodicTriggers = await _context.PeriodicTriggers.ToListAsync();

            periodicTriggers.ForEach(periodicTrigger => _triggerEvaluators.Add(periodicTrigger.Id, Evaluate(periodicTrigger)));

            await Task.Delay(1000);
        }
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
            var workflows = await _context.Workflows.Include(w => w.Triggers).Where(w => w.Triggers.Any(t => t.Id == trigger.Id)).ToListAsync();

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
