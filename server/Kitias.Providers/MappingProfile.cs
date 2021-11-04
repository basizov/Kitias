using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kitias.Providers
{
	/// <summary>
	/// Service ti map entites
	/// </summary>
	public class MappingProfile : Profile
	{
		/// <summary>
		/// Configure all entites
		/// </summary>
		public MappingProfile()
		{
			CreateMap<Person, PersonDto>();
			CreateMap<Group, GroupDto>()
				.ForMember(g => g.EducationType, o => o.MapFrom(g => GetEnumMemberAttrValue(g.EducationType)))
				.ForMember(g => g.Speciality, o => o.MapFrom(g => GetEnumMemberAttrValue(g.Speciality)));
			CreateMap<Student, StudentDto>()
				.ForMember(s => s.Course, o => o.MapFrom(s => s.Group.Course))
				.ForMember(s => s.GroupNumber, o => o.MapFrom(s => s.Group.Number))
				.ForMember(s => s.EducationType, o => o.MapFrom(s => GetEnumMemberAttrValue(s.Group.EducationType)))
				.ForMember(s => s.Speciality, o => o.MapFrom(s => GetEnumMemberAttrValue(s.Group.Speciality)));
			CreateMap<Subject, SubjectDto>()
				.ForMember(s => s.Week, o => o.MapFrom(s => GetEnumMemberAttrValue(s.Week)))
				.ForMember(s => s.Day, o => o.MapFrom(s => GetEnumMemberAttrValue(s.Day)))
				.ForMember(s => s.Course, o => o.MapFrom(s => s.Group.Course))
				.ForMember(s => s.GroupNumber, o => o.MapFrom(s => s.Group.Number))
				.ForMember(s => s.Speciality, o => o.MapFrom(s => GetEnumMemberAttrValue(s.Group.Speciality)));
			CreateMap<Teacher, TeacherDto>();
		}

		private static string GetEnumMemberAttrValue<T>(T payload)
			where T : Enum
		{
			var type = typeof(T);
			var membersInfo = type.GetMember(payload.ToString());
			var memberInfo = membersInfo.FirstOrDefault();

			if (memberInfo != null)
			{
				var attributes = memberInfo.GetCustomAttributes(false)
					.OfType<EnumMemberAttribute>();
				var attribute = attributes.FirstOrDefault();

				if (attribute != null)
					return attribute.Value;
			}
			throw new ArgumentNullException("Couldn't find memberInfo");
		}
	}
}
