using SOC.Conductor.DTOs;

namespace SOC.Conductor.Services;

public interface IWorkflowBuilderService
{
    public Task Build(CreateWorkflowDto workflowDto);
}
