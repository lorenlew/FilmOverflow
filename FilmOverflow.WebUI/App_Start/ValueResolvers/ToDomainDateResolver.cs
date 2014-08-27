using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using AutoMapper;
using FilmOverflow.WebUI.ViewModels;

namespace FilmOverflow.WebUI.App_Start.ValueResolvers
{
	public class ToDomainDateResolver : ValueResolver<SeanceViewModel, DateTime>
	{
		#region Overrides of ValueResolver<SeanceViewModel,DateTime>

		protected override DateTime ResolveCore(SeanceViewModel source)
		{
			var dateString = source.Date;
			var date = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			var domainDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

			return domainDate;
		}

		#endregion
	}
}