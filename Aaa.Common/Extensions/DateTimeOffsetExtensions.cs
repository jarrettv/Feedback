// <copyright file="DateTimeOffsetExtensions.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2009 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/29/2009 10:00:11 PM</date>
// <productName>Common</productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Summary description for DateTimeOffsetExtensions
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset ToStartOfMonth(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, 1, 0, 0, 0, 0, date.Offset);
        }

        public static DateTimeOffset ToEndOfMonth(this DateTimeOffset date)
        {
            return date.ToStartOfMonth().AddMonths(1).AddTicks(-1);
        }

        public static DateTimeOffset ToStartOfDay(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Offset);
        }

        public static DateTimeOffset ToEndOfDay(this DateTimeOffset date)
        {
            return ToStartOfDay(date).AddDays(1).AddTicks(-1);
        }

        public static bool IsToday(this DateTimeOffset date)
        {
            return date.ToStartOfDay().Subtract(DateTimeOffset.Now.ToStartOfDay()).TotalDays == 0;
        }

        public static int GetWeekOfYear(this DateTimeOffset date)
        {
            CultureInfo cult = CultureInfo.CurrentCulture;
            return cult.Calendar.GetWeekOfYear(date.UtcDateTime, cult.DateTimeFormat.CalendarWeekRule,
                cult.DateTimeFormat.FirstDayOfWeek);
        }

        public static IEnumerable<DateTimeOffset> GetDaysInWeek(this DateTimeOffset date)
        {
            //move to previous sunday
            DateTimeOffset startDate = date.AddDays(-(int)date.DayOfWeek);
            for (int i = 0; i < 7; i++)
            {
                yield return startDate.AddDays(i);
            }
        }

        public static DateTimeOffset GetSundayNearMonthsBegin(this DateTimeOffset date)
        {
            DateTimeOffset start = date.ToStartOfMonth();
            //move to previous sunday
            return start.AddDays(-(int)start.DayOfWeek);
        }

        public static IEnumerable<DateTimeOffset> GetSundaysInMonth(this DateTimeOffset date)
        {
            DateTimeOffset start = new DateTimeOffset(date.Year, date.Month, 1, 0, 0, 0, DateTimeOffset.Now.Offset);
            var sunday = date.GetSundayNearMonthsBegin();
            while (sunday < start.AddMonths(1))
            {
                yield return sunday;
                sunday = sunday.AddDays(7);
            }
        }
        
        /// <summary>
        /// Format method to be used for displaying all dates in this program
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <returns>Date formatted as a string</returns>
        public static string ToFormatString(this DateTimeOffset? date, string nullText = "")
        {
            return date.HasValue ? date.Value.ToFormatString() : nullText;
        }

        /// <summary>
        /// Format method to be used for displaying all dates in this program
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <returns>Date formatted as a string</returns>
        public static string ToFormatString(this DateTimeOffset date, string nullText = "")
        {
            return date == DateTimeOffset.MinValue ? nullText : date.DateTime.ToFormatString();
        }
    }
}
