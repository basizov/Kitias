using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities;
using Kitias.Persistence.Enums;
using Kitias.Providers.Models.Group;
using Kitias.Providers.Models.Student;
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
				.ForMember(g => g.EducationType, o => o.MapFrom(g => Helpers.GetEnumMemberAttrValue(g.EducationType)))
				.ForMember(g => g.Speciality, o => o.MapFrom(g => Helpers.GetEnumMemberAttrValue(g.Speciality)))
				.ForMember(g => g.IssueDate, o => o.MapFrom(g => g.IssueDate.ToString("dd.MM.yyyy")))
				.ForMember(g => g.ReceiptDate, o => o.MapFrom(g => g.ReceiptDate.ToString("dd.MM.yyyy")));
			CreateMap<Student, StudentDto>()
				.ForMember(s => s.Email, o => o.MapFrom(s => s.Person.Email))
				.ForMember(s => s.FullName, o => o.MapFrom(s => s.Person.FullName))
				.ForMember(s => s.Course, o => o.MapFrom(s => s.Group.Course))
				.ForMember(s => s.GroupNumber, o => o.MapFrom(s => s.Group.Number))
				.ForMember(s => s.EducationType, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Group.EducationType)))
				.ForMember(s => s.Speciality, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Group.Speciality)));
			CreateMap<Subject, SubjectDto>()
				.ForMember(s => s.Week, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Week)))
				.ForMember(s => s.Day, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Day)))
				.ForMember(s => s.Course, o => o.MapFrom(s => s.Group.Course))
				.ForMember(s => s.GroupNumber, o => o.MapFrom(s => s.Group.Number))
				.ForMember(s => s.Speciality, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Group.Speciality)));
			CreateMap<Teacher, TeacherDto>();

			#endregion

			#region FromDTO

			CreateMap<CreateGroupModel, Group>()
				.ForMember(g => g.EducationType, o => o.MapFrom(g => Helpers.GetEnumMemberFromString<EducationType>(g.EducationType)))
				.ForMember(g => g.Speciality, o => o.MapFrom(g => Helpers.GetEnumMemberFromString<Speciality>(g.Speciality)))
				.ForMember(g => g.IssueDate, o => o.MapFrom(g => DateTime.Parse(g.IssueDate)))
				.ForMember(g => g.ReceiptDate, o => o.MapFrom(g => DateTime.Parse(g.ReceiptDate)));
			CreateMap<CreateStudentModel, Person>();
			CreateMap<CreateStudentModel, Student>();

			#endregion
		}
	}
}
