namespace Kitias.Providers.Models
{
	public record Result<T>
		where T : class
	{
		public T Value { get; init; }
		public bool IsSuccess { get; init; }
		public string Error { get; init; }

		public static Result<T> OnSuccess(T value) => new()
		{
			Error = null,
			IsSuccess = true,
			Value = value
		};

		public static Result<T> OnError(string error) => new()
		{
			Error = error,
			IsSuccess = false,
			Value = null
		};
	}
}
