using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	/// <summary>
	/// The type of subjet
	/// </summary>
	public enum SubjectType
	{
		/// <summary>
		/// Lections
		/// </summary>
		[EnumMember(Value = "Лекция")]
		Lecture = 0,
		/// <summary>
		/// Practises
		/// </summary>
		[EnumMember(Value = "Практика")]
		Practise = 1,
		/// <summary>
		/// Laborotories
		/// </summary>
		[EnumMember(Value = "Лабораторная работа")]
		Laborotory = 2
	}
}
