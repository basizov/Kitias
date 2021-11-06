using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kitias.Providers
{
	/// <summary>
	/// Helpers to work with transmiters
	/// </summary>
	public static class Helpers
	{
		/// <summary>
		/// Get sting from enum member
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="payload">Enum type member</param>
		/// <returns>To string type</returns>
		public static string GetEnumMemberAttrValue<T>(T payload)
			where T : Enum
		{
			var type = typeof(T);
			var membersInfo = type.GetMember(payload.ToString());
			var memberInfo = membersInfo.SingleOrDefault();

			if (memberInfo != null)
			{
				var attributes = memberInfo.GetCustomAttributes(false)
					.OfType<EnumMemberAttribute>();
				var attribute = attributes.FirstOrDefault();

				if (attribute != null)
					return attribute.Value;
			}
			throw new ArgumentException("Couldn't find memberInfo");
		}

		/// <summary>
		/// Get enum member from string
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="payload">From which string</param>
		/// <returns>Enum type member</returns>
		public static T GetEnumMemberFromString<T>(string payload)
			where T : Enum
		{
			var type = typeof(T);

			foreach (var name in Enum.GetNames(type))
			{
				var fieldInfo = type.GetField(name);
				var fieldAttributes = (EnumMemberAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), true);
				var fieldAttribute = fieldAttributes.SingleOrDefault();

				if (fieldAttribute != null && fieldAttribute.Value == payload)
					return (T)Enum.Parse(type, name);
			}
			throw new ArgumentException("Couldn't find fieldAttribute");
		}
	}
}
