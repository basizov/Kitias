using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	/// <summary>
	/// Role different types
	/// </summary>
	public enum RoleTypes
	{
		/// <summary>
		/// Admin user
		/// </summary>
		[EnumMember(Value = RolesNames.ADMIN_ROLE)]
		Admin = 0,
		/// <summary>
		/// Student user
		/// </summary>
		[EnumMember(Value = RolesNames.STUDENT_ROLE)]
		Student = 1,
		/// <summary>
		/// Teacher user
		/// </summary>
		[EnumMember(Value = RolesNames.TEACHER_ROLE)]
		Teacher = 2,
		/// <summary>
		/// SuperAdmin user
		/// </summary>
		[EnumMember(Value = RolesNames.SUPER_ADMIN_ROLE)]
		SuperAdmin = 3
	}

	/// <summary>
	/// Class which stored all roles names
	/// </summary>
	public static class RolesNames
	{
		/// <summary>
		/// Admin role value
		/// </summary>
		public const string ADMIN_ROLE = "Admin";
		/// <summary>
		/// Student role value
		/// </summary>
		public const string STUDENT_ROLE = "Student";
		/// <summary>
		/// Teacher role value
		/// </summary>
		public const string TEACHER_ROLE = "Teacher";
		/// <summary>
		/// SuperAdmin role value
		/// </summary>
		public const string SUPER_ADMIN_ROLE = "SuperAdmin";
	}
}
