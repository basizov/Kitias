using System.Runtime.Serialization;

namespace Kitias.Persistence.Enums
{
	public enum EducationType
	{
		[EnumMember(Value = "Очное обучение")]
		Full = 0,
		[EnumMember(Value = "Заочное обучение")]
		Part = 1
	}
}
