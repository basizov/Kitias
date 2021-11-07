using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	/// <summary>
	/// Roles of the person
	/// </summary>
	public enum PersonRoles
	{
		/// <summary>
		/// Teacher person role
		/// </summary>
		[EnumMember(Value = PersonsRoles.TEACHER_PERSON_ROLE)]
		Teacher = 0,
		/// <summary>
		/// Student person role
		/// </summary>
		[EnumMember(Value = PersonsRoles.STUDENT_PERSON_ROLE)]
		Student = 1,
		/// <summary>
		/// Group person role
		/// </summary>
		[EnumMember(Value = PersonsRoles.GROUP_PERSON_ROLE)]
		Group = 2,
		/// <summary>
		/// Studsovet person role
		/// </summary>
		[EnumMember(Value = PersonsRoles.STUDSOVET_PERSON_ROLE)]
		StudSovet = 3,
		/// <summary>
		/// Spotsmen person role
		/// </summary>
		[EnumMember(Value = PersonsRoles.SPORTSMEN_PERSON_ROLE)]
		Spotsmen = 4,
		/// <summary>
		/// KiberSportsmen person role
		/// </summary>
		[EnumMember(Value = PersonsRoles.KIBERSPORTSMEN_PERSON_ROLE)]
		KiberSportsmen = 5,
		/// <summary>
		/// Hostel person role
		/// </summary>
		[EnumMember(Value = PersonsRoles.HOSTEL_PERSON_ROLE)]
		Hostel = 6
	}

	/// <summary>
	/// List of person roles
	/// </summary>
	public static class PersonsRoles
	{
		/// <summary>
		/// Teacher person role value
		/// </summary>
		public const string TEACHER_PERSON_ROLE = "Teacher";
		/// <summary>
		/// Student person role value
		/// </summary>
		public const string STUDENT_PERSON_ROLE = "Student";
		/// <summary>
		/// Group person role value
		/// </summary>
		public const string GROUP_PERSON_ROLE = "Group";
		/// <summary>
		/// StudSovet person role value
		/// </summary>
		public const string STUDSOVET_PERSON_ROLE = "StudSovet";
		/// <summary>
		/// Spotsmen person role value
		/// </summary>
		public const string SPORTSMEN_PERSON_ROLE = "Spotsmen";
		/// <summary>
		/// KiberSportsmen person role value
		/// </summary>
		public const string KIBERSPORTSMEN_PERSON_ROLE = "KiberSportsmen";
		/// <summary>
		/// Hostel person role value
		/// </summary>
		public const string HOSTEL_PERSON_ROLE = "Hostel";
	}
}
