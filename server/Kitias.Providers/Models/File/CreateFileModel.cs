namespace Kitias.Providers.Models.File
{
	/// <summary>
	/// Model to create file
	/// </summary>
	public record CreateFileModel
	{
		/// <summary>
		/// File name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// File extension
		/// </summary>
		public string Extension { get; init; }
		/// <summary>
		/// File added date
		/// </summary>
		public string Date { get; init; }
		/// <summary>
		/// Email of the sender
		/// </summary>
		public string OwnerEmail { get; init; }
		/// <summary>
		/// Path to local file storing
		/// </summary>
		public string Path { get; init; }
		/// <summary>
		/// File size
		/// </summary>
		public long Size { get; init; }
	}
}
