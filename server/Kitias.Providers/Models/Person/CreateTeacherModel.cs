namespace Kitias.Providers.Models.Person
{
	/// <summary>
	/// Model to create a teacher
	/// </summary>
	public class CreateTeacherModel
	{
		/// <summary>
		/// Person name
		/// </summary>
		public string Name { get; init; }
		/// <summary>
		/// Person surname
		/// </summary>
		public string Surname { get; init; }
		/// <summary>
		/// Person patronymic
		/// </summary>
		public string Patronymic { get; init; }
		/// <summary>
		/// Person email
		/// </summary>
		public string Email { get; init; }
	}
}
