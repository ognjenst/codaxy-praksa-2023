using AutoMapper;
using SOC.Conductor.Generated;
using SOC.Conductor.Models;

namespace SOC.Conductor.Profile
{
	public class WorkflowProfile : AutoMapper.Profile
	{
        public WorkflowProfile()
        {
			CreateMap<WorkflowDef, WorkflowResponseDto>();
			CreateMap<WorkflowTask, WorkflowTaskDto>();
		}
    }
}
