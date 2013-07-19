using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Aaa.Common
{
    using System.Web.Mvc;

    public static class Helpers
    {
        public static void AddRequired(this IList<ErrorInfo> errors, string fieldName)
        {
            errors.Add(new ErrorInfo(fieldName, string.Format("The '{0}' is required.", fieldName)));
        }
        
        public static IDictionary<string, bool> ToDictionary<T>(this T flags) where T : struct
        {
            var dict = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(x => Convert.ToInt32(x) != 0)
                .ToDictionary(x => x.ToString().SplitPascalCase(), x => flags.IsSet(x));
            return dict;
        }

        public static bool IsSet<T>(this T value, T flags) where T : struct
        {
            Type type = typeof(T);

            // only works with enums
            if (!type.IsEnum) throw new ArgumentException(
                "The type parameter T must be an enum type.");

            // handle each underlying type
            Type numberType = Enum.GetUnderlyingType(type);

            if (numberType.Equals(typeof(int)))
            {
                return Box<int>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(sbyte)))
            {
                return Box<sbyte>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(byte)))
            {
                return Box<byte>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(short)))
            {
                return Box<short>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(ushort)))
            {
                return Box<ushort>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(uint)))
            {
                return Box<uint>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(long)))
            {
                return Box<long>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(ulong)))
            {
                return Box<ulong>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(char)))
            {
                return Box<char>(value, flags, (a, b) => (a & b) == b);
            }
            else
            {
                throw new ArgumentException(
                    "Unknown enum underlying type " + numberType.Name + ".");
            }
        }

        /// Helper function for handling the value types. Boxes the
        /// params to object so that the cast can be called on them.
        private static bool Box<T>(object value, object flags,
            Func<T, T, bool> op)
        {
            return op((T)value, (T)flags);
        }

        public static string GetErrorsString(this RulesException ex)
        {
            return ("Rules Exception Errors: " +
                  String.Join("  ", ex.Errors.Select(x => String.Format("Property: '{0}', Error: {1}", x.PropertyName, x.ErrorMessage))));
        }

        /// <summary>
        /// Converts the <c>DbEntityValidationException</c> to a <c>RulesException</c>.
        /// </summary>
        /// <param name="ve"></param>
        /// <returns></returns>
        public static RulesException ToRulesException(this DbEntityValidationException ve)
        {
            return new RulesException(
                ve.EntityValidationErrors.First().ValidationErrors
                .Select(e => new ErrorInfo(e.PropertyName, e.ErrorMessage)));
        }

        /// <summary>
        /// Adds errors from a <c>RulesException</c> as model errors.
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="ex"></param>
        public static void AddRuleErrors(this ModelStateDictionary modelState, RulesException ex)
        {
            foreach (var x in ex.Errors)
            {
                modelState.AddModelError(x.PropertyName, x.ErrorMessage);
            }
        }

        /// <summary>
        /// Formats a timespan into days, hours, minutes, seconds
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Days > 0 ? string.Format("{0:0} days, ", span.Days) : string.Empty,
                span.Hours > 0 ? string.Format("{0:0} hours, ", span.Hours) : string.Empty,
                span.Minutes > 0 ? string.Format("{0:0} minutes, ", span.Minutes) : string.Empty,
                span.Seconds > 0 ? string.Format("{0:0} seconds", span.Seconds) : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            return formatted;
        }

        /// <summary>
        /// Format double values
        /// </summary>
        /// <param name="amount">Amount to display</param>
        /// <param name="format">Format string</param>
        /// <returns>Double formatted as a string</returns>
        public static string ToFormatString(this double? amount, string format)
        {
            return !amount.HasValue ? string.Empty : amount.Value.ToString(format);
        }

        /// <summary>
        /// Format double values as 0.00
        /// </summary>
        /// <param name="amount">Amount to display</param>
        /// <returns>Double formatted as a string</returns>
        public static string ToFormatString(this double? amount)
        {
            return amount.ToFormatString("0.00");
        }

        /// <summary>
        /// Format Boolean values as Yes or No
        /// </summary>
        /// <param name="value">Boolean value to format</param>
        /// <returns>Boolean formatted as a Yes/No string</returns>
        public static string ToFormatString(this bool value)
        {
            return value ? "Yes" : "No";
        }

        /// <summary>
        /// Format Boolean values as Yes or No
        /// </summary>
        /// <param name="value">Boolean value to format</param>
        /// <returns>Boolean formatted as a Yes/No string</returns>
        public static string ToFormatString(this bool? value)
        {
            return !value.HasValue ? string.Empty : 
                value.Value.ToFormatString();
        }
    }
}
