using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmOverflow.WebUI.Helpers
{
	public static class StringHelper
	{
		/// <summary>
		/// Trim the end of a string and replace with "..." if it's longer than a certain length.
		/// </summary>
		/// <param name="value">String this extends.</param>
		/// <param name="maxLength">Maximum length.</param>
		/// <returns>Ellipsis shortened string.</returns>
		public static string TrimEndIfLongerThan(this string value, int maxLength)
		{
			if (value.Length > maxLength)
			{
				return value.Substring(0, maxLength - 3) + "...";
			}

			return value;
		}
	}
}