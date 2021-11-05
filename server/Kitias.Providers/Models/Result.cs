namespace Kitias.Providers.Models
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public record Result<T>
		where T : class
	{
		/// <summary>
		/// Succes result value
		/// </summary>
		public T Value { get; init; }
		/// <summary>
		/// True if success result
		/// </summary>
		public bool IsSuccess { get; init; }
		/// <summary>
		/// Error message
		/// </summary>
		public string Error { get; init; }
	}

	/// <summary>
	/// Handler to return result
	/// </summary>
	public static class ResultHandler
	{
		/// <summary>
		/// Return success type
		/// </summary>
		/// <typeparam name="T">Return class</typeparam>
		/// <param name="value">Result value</param>
		/// <returns>Result</returns>
		public static Result<T> OnSuccess<T>(T value)
			where T : class => new()
		{
			Error = null,
			IsSuccess = true,
			Value = value
		};

		/// <summary>
		/// Failure result
		/// </summary>
		/// <typeparam name="T">Working class</typeparam>
		/// <param name="error">Error message</param>
		/// <returns>Result</returns>
		public static Result<T> OnFailure<T>(string error)
			where T : class => new()
		{
			Error = error,
			IsSuccess = false,
			Value = null
		};
	}
}
