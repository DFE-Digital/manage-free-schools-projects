using System;

namespace Dfe.ManageFreeSchoolProjects.Extensions
{
	public static class DateTimeExtensions
	{
		public static string ToUkDateString(this DateTime dateTime) => dateTime.ToString("dd/MM/yyyy");

		public static string ToYearString(this DateTime? dateTime)
		{
			string dateYear = dateTime.HasValue ? dateTime.Value.ToString("yyyy") : null;
			return dateYear;
		}
		
		public static string ToYearString(this DateTime dateTime)
		{
			return dateTime.ToString("yyyy");
		}
		
		public static string ToDateString(this DateTime? dateTime, bool includeDayOfWeek = false, bool truncateMonth = false)
		{
			if (!dateTime.HasValue)
			{
				return string.Empty;
			}
			return ToDateString(dateTime.Value, includeDayOfWeek, truncateMonth);
		}

		public static string ToDateString(this DateTime dateTime, bool includeDayOfWeek = false, bool truncateMonth = false)
		{
			if (includeDayOfWeek)
			{
				return truncateMonth ? dateTime.ToString("dddd d MMM yyyy") : dateTime.ToString("dddd d MMMM yyyy");
			}

			return truncateMonth ? dateTime.ToString("d MMM yyyy"): dateTime.ToString("d MMMM yyyy");
		}

		public static DateTime FirstOfMonth(this DateTime thisMonth, int monthsToAdd)
		{
			var month = (thisMonth.Month + monthsToAdd) % 12;
			if (month == 0) month = 12;
			var yearsToAdd = (thisMonth.Month + monthsToAdd - 1) / 12;
			return new DateTime(thisMonth.Year + yearsToAdd, month, 1);
		}
	}
}