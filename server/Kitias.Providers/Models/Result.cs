namespace Kitias.Providers.Models
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public record Result<T>
		where T : class
	{
		public T Value { get; init; }
		public bool IsSuccess { get; init; }
		public string Error { get; init; }
	}

	public static class ResultHandler
	{
		public static Result<T> OnSuccess<T>(T value)
			where T : class => new()
		{
			Error = null,
			IsSuccess = true,
			Value = value
		};

		public static Result<T> OnFailure<T>(string error)
			where T : class => new()
		{
			Error = error,
			IsSuccess = false,
			Value = null
		};
	}
}
