using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	public enum DayWeek
	{
		[EnumMember(Value = "Понедельник")]
		Monday = 0,
		[EnumMember(Value = "Вторник")]
		Tuesday = 1,
		[EnumMember(Value = "Среда")]
		Wednesday = 2,
		[EnumMember(Value = "Четверг")]
		Thursday = 3,
		[EnumMember(Value = "Пятница")]
		Friday = 4,
		[EnumMember(Value = "Суббота")]
		Saturday = 5,
		[EnumMember(Value = "Воскресенье")]
		Sunday = 6
	}
}
