namespace Kitias.Providers.Models
{
	/// <summary>
	/// Model to send emails
	/// </summary>
	public record EmailRequestModel
	{
		/// <summary>
		/// Email recipient's address
		/// </summary>
		public string To { get; init; }
		/// <summary>
		/// Email subject
		/// </summary>
		public string Subject { get; init; }
		/// <summary>
		/// Email body message
		/// </summary>
		public string Message { get; init; }
	}
}
