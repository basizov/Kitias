using System.Collections.Generic;

namespace Kitias.Providers.Models
{
	/// <summary>
	/// Model for registration user and student
	/// </summary>
	public class SignUpRegisterModel
	{
		/// <summary>
		/// New user variant
		/// </summary>
		public string PersonType { get; init; }
		/// <summary>
		/// New user email
		/// </summary>
		public string Email { get; init; }
		/// <summary>
		/// New user username
		/// </summary>
		public string UserName { get; init; }
		/// <summary>
		/// New user password
		/// </summary>
		public string Password { get; init; }
		/// <summary>
		/// Person name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Person surname
		/// </summary>
		public string Surname { get; init; }
		/// <summary>
		/// Person patronymic
		/// </summary>
		public string Patronymic { get; init; }
		/// <summary>
		/// Student group number
		/// </summary>
		public string GroupNumber { get; init; }
		/// <summary>
		/// User roles
		/// </summary>
		public IEnumerable<string> Roles { get; init; }
	}
}
