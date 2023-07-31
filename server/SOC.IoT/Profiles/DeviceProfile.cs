using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SOC.IoT.Generated;
using SOC.IoT.Handler;

namespace SOC.IoT.Profiles;

public class DeviceProfile : Profile
{
    public DeviceProfile() {
        CreateMap<DeviceRequest, DeviceUpdateDTO>()
           .ForMember(
                dest => dest.State,
                opt => opt.MapFrom(src => new DeviceState { State = src.State})
            )
           .ForMember(
                dest => dest.Light,
                opt => opt.MapFrom(src => new DeviceLight { Brightness = src.Brightness })
            )
           .ForMember(
                dest => dest.ColorXy,
                opt => opt.MapFrom(src => new DeviceColorXy { X = src.X, Y = src.Y })
            );
    }
}
