using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Aaa.Common
{
	/*
	 * Some of This Extensions were taken from http://dotnetslackers.com/articles/aspnet/5-Helpful-DateTime-Extension-Methods.aspx
	 */
	public static class DateTimeExtensions 
    {
        /// <summary>
        /// Retrives the first day of the month of the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">A date from the month we want to get the first day.</param>
        /// <returns>A DateTime representing the first day of the month.</returns>
        public static DateTime ToStartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Retrives the last day of the month of the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">A date from the month we want to get the last day.</param>
        /// <returns>A DateTime representing the last day of the month.</returns>
        public static DateTime ToEndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static DateTime ToDefaultTime(this DateTime date)
        {
            return date.Date;
        }

        public static int GetWeekOfYear(this DateTime date)
        {
            CultureInfo cult = CultureInfo.CurrentCulture;
            return cult.Calendar.GetWeekOfYear(date.Date, cult.DateTimeFormat.CalendarWeekRule,
                cult.DateTimeFormat.FirstDayOfWeek);
        }

        public static IEnumerable<DateTime> GetDaysInWeek(this DateTime date)
        {
            //move to previous sunday
            DateTime startDate = date.AddDays(-(int)date.DayOfWeek);
            for (int i = 0; i < 7; i++)
            {
                yield return startDate.AddDays(i);
            }
        }

        public static IEnumerable<DateTime> GetDaysInMonth(this DateTime date)
        {
            date = date.ToStartOfMonth();
            for (int i = 0; i < DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                yield return date.AddDays(i);
            }
        }

        public static DateTime GetSundayNearMonthsBegin(this DateTime date)
        {
            DateTime start = date.ToStartOfMonth();
            //move to previous sunday
            return start.AddDays(-(int)start.DayOfWeek);
        }

        public static IEnumerable<DateTime> GetSundaysInMonth(this DateTime date)
        {
            DateTime start = date.ToStartOfMonth();
            var sunday = date.GetSundayNearMonthsBegin();
            while (sunday < start.AddMonths(1))
            {
                yield return sunday;
                sunday = sunday.AddDays(7);
            }
        }

        /// <summary>
        /// Retrives the previous day of week for the <paramref name="date"/> or the <paramref name="date"/> when already the day of the week.
        /// </summary>
        /// <param name="date">A date.</param>
        /// <param name="dayOfweek">The kind of DayOfWeek we want to get.</param>
        /// <returns>A DateTime representing the nearest day of the week.</returns>
        public static DateTime ToCurrentOrPrevDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            if (date.DayOfWeek == dayOfWeek) return date;
            return date.ToPrevDayOfWeek(dayOfWeek);
        }

        /// <summary>
        /// Retrives the previous day of week for the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">A date.</param>
        /// <param name="dayOfweek">The kind of DayOfWeek we want to get.</param>
        /// <returns>A DateTime representing the nearest day of the week.</returns>
        public static DateTime ToPrevDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            do date = date.AddDays(-1);
            while (date.DayOfWeek != dayOfWeek);
            return date;
        }

        /// <summary>
        /// Retrives the next day of week for the <paramref name="date"/> or the <paramref name="date"/> when already the day of the week.
        /// </summary>
        /// <param name="date">A date.</param>
        /// <param name="dayOfweek">The kind of DayOfWeek we want to get.</param>
        /// <returns>A DateTime representing the nearest day of the week.</returns>
        public static DateTime ToCurrentOrNextDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            if (date.DayOfWeek == dayOfWeek) return date;
            return date.ToNextDayOfWeek(dayOfWeek);
        }

        /// <summary>
        /// Retrives the next day of the week that will occour after <paramref name="date"/>.
        /// </summary>
        /// <remarks>If <paramref name="name"/>.DayOfWeek is already <paramref name="dayOfWeek"/>, it will return the next one (seven days after)</remarks>
        /// <param name="date">A date.</param>
        /// <param name="dayOfWeek">The kind of DayOfWeek we want to get.</param>
        /// <returns>A DateTime representing the next day of the week that will occour after.</returns>
        public static DateTime ToNextDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            do date = date.AddDays(1);
                while (date.DayOfWeek != dayOfWeek);
            return date;
        }

        public static DateTime ToLastDayOfWeekOfTheMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            date = date.ToEndOfMonth();
            while (date.DayOfWeek != dayOfWeek)
                date = date.AddDays(-1);
            return date;
        }

        public static DateTime ToFirstDayOfWeekOfTheMonth(this DateTime date, DayOfWeek dayOfweek)
        {
            DateTime firstDayOfTheMonth = date.ToStartOfMonth();
            if (firstDayOfTheMonth.DayOfWeek == dayOfweek)
            {
                return firstDayOfTheMonth;
            }
            return firstDayOfTheMonth.ToNextDayOfWeek(dayOfweek);
        }

        /// <summary>
        /// Format method to be used for displaying all dates in this program
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <returns>Date formatted as a string</returns>
        public static string ToFormatString(this DateTime? date, string nullText = "")
        {
            return date.HasValue ? date.Value.ToFormatString() : nullText;
        }

        /// <summary>
        /// Format method to be used for displaying all dates in this program
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <returns>Date formatted as a string</returns>
        public static string ToFormatString(this DateTime date, string nullText = "")
        {
            if (date == default(DateTime)) return nullText;
            return date.ToString("MM/dd/yyyy");
        }
	}
}