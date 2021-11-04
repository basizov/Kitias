using System;

namespace Kitias.Persistence.DTOs
{
	/// <summary>
	/// Student data transfer object
	/// </summary>
	public record StudentDto
	{
		/// <summary>
		/// Student identifier
		/// </summary>
		public Guid Id { get; init; }
		/// <summary>
		/// Student e-mail
		/// </summary>
		public string Email { get; init; }
		/// <summary>
		/// Student FIO
		/// </summary>
		public string FullName { get; init; }
		/// <summary>
		/// Student course
		/// </summary>
		public byte Course { get; init; }
		/// <summary>
		/// Student group number
		/// </summary>
		public string GroupNumber { get; init; }
		/// <summary>
		/// Student education type
		/// </summary>
		public string EducationType { get; init; }
		/// <summary>
		/// Student speciality
		/// </summary>
		public string Speciality { get; init; }
	}
}
