namespace Kitias.Providers
{
	/// <summary>
	/// Model to view for user
	/// </summary>
	public record ExceptionModel
	{
		/// <summary>
		/// Error status code
		/// </summary>
		public int StatusCode { get; init; }
		/// <summary>
		/// Error message
		/// </summary>
		public string Message { get; init; }
		/// <summary>
		/// Stack trace
		/// </summary>
		public string From { get; init; }
	}
}
