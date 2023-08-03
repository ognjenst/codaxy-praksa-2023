using AutoMapper;
using SOC.IoT.Domain.Entity;
using SOC.IoT.Handler;

namespace SOC.IoT.Profiles;

public class DeviceProfile : Profile
{
    public DeviceProfile()
    {
        //CreateMap<DeviceRequest, DeviceUpdateDTO>()
        //    .ForMember(
        //        dest => dest.State,
        //        opt => opt.MapFrom(src => new DeviceState { State = src.State })
        //    )
        //    .ForMember(
        //        dest => dest.Light,
        //        opt => opt.MapFrom(src => new DeviceLight { Brightness = src.Brightness })
        //    )
        //    .ForMember(
        //        dest => dest.ColorXy,
        //        opt => opt.MapFrom(src => new DeviceColorXy { X = src.X, Y = src.Y })
        //    );

        CreateMap<DeviceRequest, Device>()
            .ForMember(
                dest => dest.State,
                opt => opt.MapFrom(src => new DeviceState { State = src.State })
            )
            .ForMember(
                dest => dest.Light,
                opt => opt.MapFrom(src => new DeviceLight { Brightness = (decimal)src.Brightness })
            )
            .ForMember(
                dest => dest.ColorXy,
                opt =>
                    opt.MapFrom(src => new DeviceColorXy { X = (decimal)src.X, Y = (decimal)src.Y })
            );
    }
}
