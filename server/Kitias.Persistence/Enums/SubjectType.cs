using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	public enum SubjectType
	{
		[EnumMember(Value = "Лекция")]
		Lecture = 0,
		[EnumMember(Value = "Практика")]
		Practise = 1,
		[EnumMember(Value = "Лабораторная работа")]
		Laborotory = 2
	}
}
