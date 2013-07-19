// <copyright file="StringHelper.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2012 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>8/9/2012 4:34:26 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringHelper
    {
        /// <summary>
        /// Returns the index of the Nth occurrence of a string
        /// Code from: http://stackoverflow.com/questions/186653/c-sharp-indexof-the-nth-occurrence-of-a-string
        /// </summary>
        public static int IndexOfOccurence(this string s, string match, int occurence)
        {
            int i = 1;
            int index = 0;
            while (i <= occurence && (index = s.IndexOf(match, index + 1)) != -1)
            {
                if (i == occurence) return index;
                i++;
            }

            return -1;
        }

        public static string SplitPascalCase(this string str)
        {
            return Regex.Replace(str, "(\\B[A-Z]+|[0-9]+)", " $1");
        }

        public static string RemoveNumbers(this string str)
        {
            var chars = str.ToCharArray()
                .Where(x => !char.IsDigit(x));
            return new string(chars.ToArray());
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            //remove invalid file name chars
            str = new string(str.ToCharArray().Where(c => !System.IO.Path.GetInvalidFileNameChars().Contains(c)).ToArray());

            //remove url special chars ;/?:@&=+$,()|\^[]'<>#%"
            str = new string(str.ToCharArray().Where(c => !";/?:@&=+$,()|\\^[]'<>#%\"".Contains(c)).ToArray());

            //normalize and remove non-spacing marks
            //TODO: is this really necessary?
            str = str.Normalize(NormalizationForm.FormD);
            str = new string(str.ToCharArray().Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());

            //WCF doesn't support periods and it may throw off IIS6 or other extension mime type issues
            str = str.Replace(".", string.Empty);
            return str;
        }

        public static int ExtractNumber(this string str)
        {
            var chars = str.ToCharArray();
            var nums = new List<char>();
            foreach (var c in chars)
                if (char.IsNumber(c)) nums.Add(c);

            if (nums.Count > 0) return int.Parse(new string(nums.ToArray()));
            return 0;
        }

        /// <summary>
        /// Truncates a string to the specified length
        /// </summary>
        /// <param name="str">String to truncate</param>
        /// <param name="length">Resulting maximum string length</param>
        /// <returns>String truncated to the specified length</returns>
        public static string Truncate(this string str, int length)
        {
            return (str == null ? string.Empty : str.Length > length ? str.Substring(0, length) : str);
        }
        
        /// <summary>
        /// Shorten input text to a desired length, preserving words and appending the
        /// specified trailer.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <param name="trailer"></param>
        /// <returns>A string with the trailer attached if the string greater than length</returns>
        public static string AbbreviateText(this string input, int length, string trailer = "…")
        {
            if (string.IsNullOrEmpty(input))
            throw new ArgumentNullException("input");

            if (null == trailer)
            throw new ArgumentNullException("trailer");

            StringBuilder output = new StringBuilder(length + 20); //Add room for a word not breaking and the trailer			

            string[] words = input.Split(new char[] { ' ' });
            int i = 0;
            while (((output.Length + words[i].Length + trailer.Length) < (length - trailer.Length))
                    && (i < words.GetUpperBound(0)))
            {
                output.Append(words[i]);
                output.Append(" ");
                i++;
            }

            if (i < words.GetUpperBound(0)) //We exited the loop before reaching the end of the array - which would normally be the case.
            {
                output.Remove(output.Length - 1, 1); //Remove the ending space before attaching the trailer.
                output.Append(trailer);
            }
            else
            {
                output.Append(words[i]);
            }

            return output.ToString();
        }

        /// <summary>
        /// Trims the string or returns empty string if the value is null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
        }

        /// <summary>
        /// Trims the string or returns null string if the value is empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimOrNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? default(string) : value.Trim();
        }

        public static IEnumerable<int> SplitToInt32(this string str, char separtor)
        {
            if (string.IsNullOrEmpty(str))
                return Enumerable.Empty<int>();
            return str.Split(new[] { separtor }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x));
        }


        /// <summary>
        /// Parses the given string for two string values that equate to a valid date range.
        /// </summary>
        /// <param name="value">string value of two date values separated by a hyphen</param>
        /// <returns>a Range object if parse was successful, null otherwise</returns>
        public static Range<DateTime> ParseDateTimeRange(this string value)
        {
            DateTime start, end;
            if (value.Contains("-") && DateTime.TryParse(value.Split('-')[0], out start) && DateTime.TryParse(value.Split('-')[1], out end))
                return new Range<DateTime>(start, end);
            return null;
        }

        /// <summary>
        /// Parses the given string for two string values that equate to a valid date range.
        /// </summary>
        /// <param name="value">string value of two date values separated by a hyphen</param>
        /// <returns>a Range object if parse was successful, null otherwise</returns>
        public static Range<DateTimeOffset> ParseDateTimeOffsetRange(this string value)
        {
            DateTimeOffset start, end;
            if (value.Contains("-") && DateTimeOffset.TryParse(value.Split('-')[0], out start) && DateTimeOffset.TryParse(value.Split('-')[1], out end))
                return new Range<DateTimeOffset>(start.ToStartOfDay(), end.ToEndOfDay());
            return null;
        }

        /// <summary>
        /// Returns the last four characters of the string; if null, returns empty string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string LastFour(this string value)
        {
            return String.IsNullOrEmpty(value) ? ""
                : value.Length <= 4 ? value
                : value.Substring(value.Length - 4, 4);
        }

        /// <summary>
        /// Returns true if numeric; otherwise, returns false.
        /// Used for ordering alphanumeric strings.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string value)
        {
            int number;
            return Int32.TryParse(value, out number);
        }

        /// <summary>
        /// Splits a string containing emails separated by the following characters: [;,\t\r\n]
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public static IList<string> SplitEmails(string emails)
        {
            return Regex.Replace(emails ?? "", "[ ,\t\r\n;]+", ";").Split(';').ToList();
        }

        public static string MacroReplace(this string str)
        {
            // replace %date% macro with a filename friendly formatted date
            str = str.Replace("%date%", DateTime.Now.ToString("yyyyMMddHHmmssfffffff"));

            // replace %id% macro with a uniquie identifier
            str = str.Replace("%id%", Guid.NewGuid().ToString());

            return str;
        }
    }
}
