using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using AutoMapper;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI.App_Start.ValueResolvers
{
	public class ToDomainTimeResolver : ValueResolver<SeanceViewModel, DateTime>
	{
		#region Overrides of ValueResolver<SeanceViewModel,DateTime>

		protected override DateTime ResolveCore(SeanceViewModel source)
		{
			var timeString = source.Time;
			var time = DateTime.ParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture);
			var dateMin = new DateTime(1941, 6, 22);
			var domainTime = new DateTime(dateMin.Year, dateMin.Month, dateMin.Day, time.Hour, time.Minute, 0);

			return domainTime;
		}

		#endregion
	}
}