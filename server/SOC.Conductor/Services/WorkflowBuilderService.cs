using IoT.Conductor.Services;
using SOC.Conductor.DTOs;

namespace SOC.Conductor.Services
{
    public class WorkflowBuilderService : IWorkflowBuilderService
    {
        public IMetadataResourceClient _metadataResourceClient { get; set; }
        public WorkflowBuilderService(IMetadataResourceClient metadataResourceClient) { _metadataResourceClient = metadataResourceClient; }
        public async System.Threading.Tasks.Task Build(CreateWorkflowDto workflowDto)
        {
            var tasks = new List<WorkflowTask>();
            
            foreach (var task in workflowDto.Tasks)
            {
                //Decision task wrapper
                tasks.Add(new WorkflowTask()
                {
                    Name = task.Name,
                    TaskReferenceName = task.TaskReferenceName,
                    InputParameters = task.InputParameters,
                    Type = task.Type,
                });
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

            //Check
            try
            {
                await _metadataResourceClient.UpdateAsync(new List<WorkflowDef> { workflowDef });
            } catch (Exception) { }
        }
    }
}
