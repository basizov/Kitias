using AutoMapper;
using Kitias.Persistence.DTOs;
using Kitias.Persistence.Entities.Scheduler;
using Kitias.Persistence.Entities.People;
using Kitias.Persistence.Enums;
using Kitias.Providers.Models.Group;
using Kitias.Providers.Models.Person;
using Kitias.Providers.Models.Subject;
using System;
using Kitias.Persistence.Entities.File;
using System.Linq;
using Kitias.Persistence.Entities.Scheduler.Attendence;
using Kitias.Providers.Models.Attendances;
using System.Globalization;

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
			CreateMap<Group, GroupNames>();
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
				.ForMember(s => s.Time, o => o.MapFrom(s => s.Time.ToString()))
				.ForMember(s => s.Date, o => o.MapFrom(s => s.Date.ToString("dd.MM.yyyy")))
				.ForMember(s => s.Day, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Day)));
			CreateMap<Teacher, TeacherDto>()
				.ForMember(s => s.Email, o => o.MapFrom(s => s.Person.Email))
				.ForMember(s => s.FullName, o => o.MapFrom(s => s.Person.FullName));
			CreateMap<File, FileDto>()
				.ForMember(s => s.FullName, o => o.MapFrom(s => $"{s.Name}.{s.Extension}"))
				.ForMember(s => s.Date, o => o.MapFrom(s => s.Date.ToString("dd.MM.yyyy")))
				.ForMember(s => s.Size, o => o.MapFrom(s => Helpers.GetFileSizeFromNumber(s.Size)));
			CreateMap<Post, PostDto>()
				.ForMember(s => s.Date, o => o.MapFrom(s => s.Date.ToString("dd.MM.yyyy")))
				.ForMember(s => s.Filter, o => o.MapFrom(s => s.Filter.Select(f => Helpers.GetEnumMemberAttrValue(f))));
			CreateMap<Store, StoreDto>()
				.ForMember(s => s.Size, o => o.MapFrom(s => $"{Helpers.GetFileSizeFromNumber(s.ActualSize)} / {Helpers.GetFileSizeFromNumber(s.MaxSize)}"));
			CreateMap<Attendance, SubjectTypeInfo>()
				.ForMember(s => s.Score, o => o.MapFrom(s => s.Score.ToString()))
				.ForMember(s => s.Attended, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Attended)));
			CreateMap<Attendance, AttendanceDto>()
				.ForMember(s => s.Date, o => o.MapFrom(s => s.Subject.Date.ToString("dd.MM.yyyy")))
				.ForMember(s => s.Theme, o => o.MapFrom(s => s.Subject.Theme))
				.ForMember(s => s.Score, o => o.MapFrom(s => s.Score.ToString()))
				.ForMember(s => s.FullName, o => o.MapFrom(s => s.StudentName ?? s.Student.Person.FullName))
				.ForMember(s => s.Type, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Subject.Type)))
				.ForMember(s => s.Attended, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Attended)));
			CreateMap<AttendanceSheduler, AttendanceShedulerDto>()
				.ForMember(s => s.TeacherFullName, o => o.MapFrom(s => s.Teacher.Person.FullName))
				.ForMember(s => s.GroupNumber, o => o.MapFrom(s => s.Group.Number));
			CreateMap<StudentAttendance, StudentAttendanceDto>()
				.ForMember(s => s.Raiting, o => o.MapFrom(s => s.Raiting.ToString()))
				.ForMember(s => s.Grade, o => o.MapFrom(s => Helpers.GetEnumMemberAttrValue(s.Grade)))
				.ForMember(s => s.StudentName, o => o.MapFrom(s => s.StudentName ?? s.Student.Person.FullName));

			#endregion

			#region ToModels

			CreateMap<AttendanceSheduler, ShedulersListResult>()
				.ForMember(s => s.GroupNumber, o => o.MapFrom(s => s.Group.Number));

			#endregion

			#region FromModel

			CreateMap<CreateGroupModel, Group>()
				.ForMember(g => g.EducationType, o => o.MapFrom(g => Helpers.GetEnumMemberFromString<EducationType>(g.EducationType)))
				.ForMember(g => g.Speciality, o => o.MapFrom(g => Helpers.GetEnumMemberFromString<Speciality>(g.Speciality)))
				.ForMember(g => g.IssueDate, o => o.MapFrom(g => DateTime.Parse(g.IssueDate)))
				.ForMember(g => g.ReceiptDate, o => o.MapFrom(g => DateTime.Parse(g.ReceiptDate)));
			CreateMap<CreateStudentModel, Person>();
			CreateMap<CreateStudentModel, Student>();
			CreateMap<CreateTeacherModel, Person>();
			CreateMap<CreateTeacherModel, Teacher>();
			CreateMap<CreateSubjectModel, Subject>()
				.ForMember(s => s.Time, o => o.MapFrom(s => TimeSpan.Parse(s.Time)))
				.ForMember(s => s.Date, o => o.MapFrom(s => DateTime.ParseExact(s.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)));
			CreateMap<UpdateSubjectModel, Subject>()
				.ForMember(s => s.Time, o => o.MapFrom(s => TimeSpan.Parse(s.Time)))
				.ForMember(s => s.Date, o => o.MapFrom(s => DateTime.ParseExact(s.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)));

			#endregion
		}
	}
}
