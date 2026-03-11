using AutoMapper;
using Domain.Models;
using Domain.Services.UserModule.DTO;
using UserApplication.API.UserModule.DTO;

namespace UserApplication.Extensions
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Users,UserDTO>().ReverseMap();	
			CreateMap<SignupRequest,UserDTO>().ReverseMap();	
		}
	}
}

