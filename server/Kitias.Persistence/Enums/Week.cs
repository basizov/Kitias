using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	public enum Week
	{
		[EnumMember(Value = "Еженедельно")]
		Every = 0,
		[EnumMember(Value = "Четная неделя")]
		Even = 1,
		[EnumMember(Value = "Нечетная неделя")]
		Odd = 2
	}
}
