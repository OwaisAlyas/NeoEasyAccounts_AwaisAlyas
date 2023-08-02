using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Neo.EasyAccounts.Web.UI.Helpers
{
	public static class EnumHelper
	{
		/// <summary>
		/// Retrieve the description on the enum, e.g.
		/// [Description("Bright Pink")]
		/// BrightPink = 2,
		/// Then when you pass in the enum, it will retrieve the description
		/// The description attribute should be on first attribute defined attribute if any other attributes defined.
		/// </summary>
		/// <param name="en">The Enumeration</param>
		/// <returns>A string representing the friendly name</returns>
		public static string Description(this Enum en)
		{
			Type type = en.GetType();

			MemberInfo[] memInfo = type.GetMember(en.ToString());

			if (memInfo != null && memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attrs != null && attrs.Length > 0) return ((DescriptionAttribute)attrs[0]).Description;
			}

			return en.ToString();
		}

		public static T GetDescription<T>(string description)
		{
			var type = typeof(T);
			if (!type.IsEnum) throw new InvalidOperationException();
			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attribute != null)
				{
					if (attribute.Description == description) return (T)field.GetValue(null);
				}
				else
				{
					if (field.Name == description) return (T)field.GetValue(null);
				}
			}
			throw new ArgumentException("Not found.", "description");
			// or return default(T);
		}

		public static T ToEnum<T>(this string value) where T : struct, IConvertible
		{
			T d = default(T);

			if (!(value != null && value.Length > 0)) return d;

			T result;

			return Enum.TryParse<T>(value, true, out result) ? result : d;
		}
	}
}