// <copyright file="VersionExtensions.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2009 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>10/25/2009 1:03:08 PM</date>
// <productName>Common</productName>
namespace Aaa.Common
{
    using System;

    public static class VersionExtensions
    {
        /// <summary>
        /// When the version is set to Major.Minor.* we can calculate the build date from the revision and build numbers.
        /// </summary>
        /// <param name="version">The version to calculuate the build date from.</param>
        /// <returns>The build date.</returns>
        public static DateTime GetBuildDate(this Version version)
        {
            DateTime date = new DateTime(2000, 1, 1);
            date = date.AddDays(version.Build);
            date = date.AddSeconds(version.Revision * 2);
            if (System.TimeZoneInfo.Local.IsDaylightSavingTime(date)) date = date.AddHours(1);
            return date;
        }
    }
}
