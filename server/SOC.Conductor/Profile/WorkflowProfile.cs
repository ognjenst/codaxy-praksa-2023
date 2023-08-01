using AutoMapper;
using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;
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
            CreateMap<PlayRequestDto, StartWorkflowRequest>();
            CreateMap<Entities.Workflow, WorkflowDto>()
				.ForMember(e => e.CreateDate, m => m.MapFrom(e => e.CreatedAt))
                .ForMember(e => e.UpdateDate, m => m.MapFrom(e => e.UpdatedAt));
		}
    }
}
