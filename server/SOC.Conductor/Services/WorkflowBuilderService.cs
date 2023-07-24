using SOC.Conductor.DTOs;
using SOC.Conductor.DTOs.Enums;
using SOC.Conductor.Generated;
using System.Threading.Tasks;

namespace SOC.Conductor.Services;

public class WorkflowBuilderService : IWorkflowBuilderService
{
    private readonly IMetadataResourceClient _metadataResourceClient;
    private readonly ILogger<WorkflowBuilderService> _logger;

    private static readonly string SIMPLE_TASK = "SIMPLE";
    private static readonly string SWITCH_TASK = "SWITCH";
    private static readonly string JS_EVALUATOR = "javascript";
    private static readonly string CONDUCTOR_MAIL = "praksa@codaxy.com";
    public WorkflowBuilderService(IMetadataResourceClient metadataResourceClient, ILogger<WorkflowBuilderService> logger) 
    { 
        _metadataResourceClient = metadataResourceClient;
        _logger = logger;
    }

    public async System.Threading.Tasks.Task Build(CreateWorkflowDto workflowDto)
    {
        var tasks = new List<WorkflowTask>();

        foreach(var item in workflowDto.Tasks.Select((value, index) => (value, index))) {
            var task = item.value;

            if (task.Expression is null) 
                AddSimpleTaskTolist(tasks, task);
            else 
                AddSwitchTaskToList(tasks, task, $"switch_task_{item.index}");
        }

        var workflowDef = new WorkflowDef()
        {
            Name = workflowDto.Name,
            Description = workflowDto.Description,
            Version = workflowDto.Version,
            OwnerEmail = CONDUCTOR_MAIL,
            TimeoutSeconds = 0,
            Tasks = tasks
        };

        try
        {
            await _metadataResourceClient.UpdateAsync(new List<WorkflowDef> { workflowDef });
        } catch (Exception ex) 
        {
            _logger.LogError($"Registering workflow on conductor failed. {ex.Message}");
            throw;
        }
    }

    private void AddSimpleTaskTolist(List<WorkflowTask> list, CreateTaskDto task)
    {
        list.Add(new WorkflowTask()
        {
            Name = task.Name,
            TaskReferenceName = task.TaskReferenceName,
            InputParameters = task.InputParameters,
            Type = SIMPLE_TASK,
        });
    }

    private void AddSwitchTaskToList(List<WorkflowTask> list, CreateTaskDto task, string taskName)
    {
        list.Add(new WorkflowTask()
        {
            Name = taskName,
            TaskReferenceName = taskName,
            InputParameters = task.ConditionInputParameters,
            Type = SWITCH_TASK,
            EvaluatorType = JS_EVALUATOR,
            Expression = task.Expression,
            DecisionCases = new Dictionary<string, ICollection<WorkflowTask>> {
                { "true", new List<WorkflowTask>
                    { new WorkflowTask()
                        {
                            Name = task.Name,
                            TaskReferenceName = task.TaskReferenceName,
                            InputParameters = task.InputParameters,
                            Type = SIMPLE_TASK,
                        }
                    }
                }
            }
        });
    }
}
