using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities;
using Kitias.Persistence.Enums;
using Kitias.Providers.Models.Group;
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
			#region ToDTO

			CreateMap<Person, PersonDto>();
			CreateMap<Group, GroupDto>()
				.ForMember(g => g.EducationType, o => o.MapFrom(g => GetEnumMemberAttrValue(g.EducationType)))
				.ForMember(g => g.Speciality, o => o.MapFrom(g => GetEnumMemberAttrValue(g.Speciality)))
				.ForMember(g => g.IssueDate, o => o.MapFrom(g => g.IssueDate.ToString("dd.MM.yyyy")))
				.ForMember(g => g.ReceiptDate, o => o.MapFrom(g => g.ReceiptDate.ToString("dd.MM.yyyy")));
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

			#endregion

			#region FromDTO

			CreateMap<CreateGroupModel, Group>()
				.ForMember(g => g.EducationType, o => o.MapFrom(g => GetEnumMemberFromString<EducationType>(g.EducationType)))
				.ForMember(g => g.Speciality, o => o.MapFrom(g => GetEnumMemberFromString<Speciality>(g.Speciality)))
				.ForMember(g => g.IssueDate, o => o.MapFrom(g => DateTime.Parse(g.IssueDate)))
				.ForMember(g => g.ReceiptDate, o => o.MapFrom(g => DateTime.Parse(g.ReceiptDate)));

			#endregion
		}

		private static string GetEnumMemberAttrValue<T>(T payload)
			where T : Enum
		{
			var type = typeof(T);
			var membersInfo = type.GetMember(payload.ToString());
			var memberInfo = membersInfo.SingleOrDefault();

			if (memberInfo != null)
			{
				var attributes = memberInfo.GetCustomAttributes(false)
					.OfType<EnumMemberAttribute>();
				var attribute = attributes.FirstOrDefault();

				if (attribute != null)
					return attribute.Value;
			}
			throw new ArgumentException("Couldn't find memberInfo");
		}
		private static T GetEnumMemberFromString<T>(string payload)
			where T : Enum
		{
			var type = typeof(T);

			foreach (var name in Enum.GetNames(type))
			{
				var fieldInfo = type.GetField(name);
				var fieldAttributes = (EnumMemberAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), true);
				var fieldAttribute = fieldAttributes.SingleOrDefault();

				if (fieldAttribute != null && fieldAttribute.Value == payload)
					return (T)Enum.Parse(type, name);
			}
			throw new ArgumentException("Couldn't find fieldAttribute");
		}
	}
}
