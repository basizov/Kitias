using System;

namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Nodel to create sheduler
	/// </summary>
	public record ShedulerRequestModel
	{
		/// <summary>
		/// Group id
		/// </summary>
		public Guid? GroupNumber { get; init; }
		/// <summary>
		/// Sheduler name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string SubjectName { get; init; }
	}

	/// <summary>
	/// Model to create sheduler with Email
	/// </summary>
	public record ShedulerProviderRequestModel
	{
		/// <summary>
		/// Teacher email
		/// </summary>
		public string TeacherEmail { get; init; }
		/// <summary>
		/// Group number
		/// </summary>
		public Guid? GroupNumber { get; init; }
		/// <summary>
		/// Sheduler name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Subject name
		/// </summary>
		public string SubjectName { get; init; }
	}
}
