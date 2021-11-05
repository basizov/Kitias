using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	/// <summary>
	/// Type of colldege education
	/// </summary>
	public enum EducationType
	{
		/// <summary>
		/// 5 time for week
		/// </summary>
		[EnumMember(Value = "Очное обучение")]
		Full = 0,
		/// <summary>
		/// 2 month for 6 months
		/// </summary>
		[EnumMember(Value = "Заочное обучение")]
		Part = 1
	}
}
