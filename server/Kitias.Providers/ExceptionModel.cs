namespace Kitias.Providers
{
	public record ExceptionModel
	{
		public int StatusCode { get; init; }
		public string Message { get; init; }
		public string From { get; init; }
	}
}
