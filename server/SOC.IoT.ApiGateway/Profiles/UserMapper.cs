using AutoMapper;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Models.Requests;

namespace SOC.IoT.ApiGateway.Profiles
{
	public class UserMapper : Profile
	{
        public UserMapper()
        {
            CreateMap<RegisterRequest, User>();
        }
    }
}
