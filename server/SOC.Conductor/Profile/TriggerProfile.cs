using SOC.Conductor.DTOs;
using SOC.Conductor.Entities;

namespace SOC.Conductor.Profile
{
    public class TriggerProfile : AutoMapper.Profile
    {
        public TriggerProfile()
        {
            CreateMap<PeriodicTrigger, CommonTriggerDto>();
            CreateMap<IoTTrigger, CommonTriggerDto>();
        }
    }
}
