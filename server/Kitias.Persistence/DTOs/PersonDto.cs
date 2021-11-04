using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Person data transfer object
	/// </summary>
	public record PersonDto
	{
		/// <summary>
		/// Person identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Person e-mail
		/// </summary>
		public string Email { get; init; }
		/// <summary>
		/// Person FIO
		/// </summary>
		public string FullName { get; init; }
	}
}
