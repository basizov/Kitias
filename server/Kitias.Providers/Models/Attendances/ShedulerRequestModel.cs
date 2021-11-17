﻿namespace Kitias.Providers.Models.Attendances
{
	/// <summary>
	/// Nodel to create sheduler
	/// </summary>
	public record ShedulerRequestModel
	{
		/// <summary>
		/// Group number
		/// </summary>
		public string GroupNumber { get; init; }
		/// <summary>
		/// Sheduler name
		/// </summary>
		public string Name { get; init; }
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
		public string GroupNumber { get; init; }
		/// <summary>
		/// Sheduler name
		/// </summary>
		public string Name { get; init; }
	}
}