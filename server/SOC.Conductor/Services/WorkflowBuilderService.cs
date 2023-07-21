using SOC.Conductor.DTOs;
using SOC.Conductor.DTOs.Enums;
using SOC.Conductor.Generated;

namespace SOC.Conductor.Services
{
    public class WorkflowBuilderService : IWorkflowBuilderService
    {
        public IMetadataResourceClient _metadataResourceClient { get; set; }
        public WorkflowBuilderService(IMetadataResourceClient metadataResourceClient) { _metadataResourceClient = metadataResourceClient; }
        public async System.Threading.Tasks.Task Build(CreateWorkflowDto workflowDto)
        {
            var tasks = new List<WorkflowTask>();
            
            for (int i = 0; i < workflowDto.Tasks.Count; i++)
            {
                var task = workflowDto.Tasks[i];
                if (task.Type == TaskType.SWITCH.ToString())
                {
                    if (i + 1 < workflowDto.Tasks.Count)
                    {
                        var nextTask = workflowDto.Tasks[i + 1];
                        tasks.Add(new WorkflowTask()
                        {
                            Name = task.Name,
                            TaskReferenceName = task.TaskReferenceName,
                            InputParameters = task.InputParameters,
                            Type = task.Type,
                            EvaluatorType = task.EvaluatorType,
                            Expression = task.Expression,
                            DecisionCases = new Dictionary<string, List<WorkflowTask>> {
                            { "true", new List<WorkflowTask>
                                { new WorkflowTask()
                                    {
                                        Name = nextTask.Name,
                                        TaskReferenceName = nextTask.TaskReferenceName,
                                        InputParameters = nextTask.InputParameters,
                                        Type = nextTask.Type,
                                    }
                                }
                            }
                        }
                        });
                        i++;
                    }
                } else
                {
                    tasks.Add(new WorkflowTask()
                    {
                        Name = task.Name,
                        TaskReferenceName = task.TaskReferenceName,
                        InputParameters = task.InputParameters,
                        Type = task.Type,
                    });
                }
            }


            var workflowDef = new WorkflowDef()
            {
                Name = workflowDto.Name,
                Description = workflowDto.Description,
                Version = workflowDto.Version,
                Restartable = workflowDto.Restartable,
                WorkflowStatusListenerEnabled = workflowDto.WorkflowStatusListenerEnabled,
                OwnerEmail = workflowDto.OwnerEmail,
                TimeoutPolicy = (WorkflowDefTimeoutPolicy) Enum.Parse(typeof(WorkflowDefTimeoutPolicy), workflowDto.TimeoutPolicy),
                TimeoutSeconds = workflowDto.TimeoutSeconds,
                Tasks = tasks
            };

            try
            {
                await _metadataResourceClient.UpdateAsync(new List<WorkflowDef> { workflowDef });
            } catch (Exception) { }
        }
    }
}
