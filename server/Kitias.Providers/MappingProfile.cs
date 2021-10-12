using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Models;

namespace Kitias.Providers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(s => s.FullName, o => o.MapFrom(s => $"{s.Surname} {s.Name} {s.Patronymic}"));
		}
	}
}
