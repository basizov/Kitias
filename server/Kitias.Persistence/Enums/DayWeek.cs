using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	/// <summary>
	/// Days of the week
	/// </summary>
	public enum DayWeek
	{
		/// <summary>
		/// Monday
		/// </summary>
		[EnumMember(Value = "Понедельник")]
		Monday = 0,
		/// <summary>
		/// Tuesday
		/// </summary>
		[EnumMember(Value = "Вторник")]
		Tuesday = 1,
		/// <summary>
		/// Wednesday
		/// </summary>
		[EnumMember(Value = "Среда")]
		Wednesday = 2,
		/// <summary>
		/// Thursday
		/// </summary>
		[EnumMember(Value = "Четверг")]
		Thursday = 3,
		/// <summary>
		/// Friday
		/// </summary>
		[EnumMember(Value = "Пятница")]
		Friday = 4,
		/// <summary>
		/// Saturday
		/// </summary>
		[EnumMember(Value = "Суббота")]
		Saturday = 5,
		/// <summary>
		/// Sunday
		/// </summary>
		[EnumMember(Value = "Воскресенье")]
		Sunday = 6
	}
}
