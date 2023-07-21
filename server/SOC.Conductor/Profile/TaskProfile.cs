using AutoMapper;
using SOC.Conductor.Generated;
using SOC.Conductor.Models;

namespace SOC.Conductor.Profile
{
    public class TaskProfile : AutoMapper.Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskDef, TaskResponseDto>();
        }
    }
}
