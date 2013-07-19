// http://eliaszone.blogspot.com/2010/11/useful-datareader-extension-method.html
namespace Aaa.Common
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public static class DataReaderExtensions
    {
        public static string ToString(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
                return reader[column].ToString();
            else
                return "";
        }
        public static Boolean ToBool(this IDataReader reader, string column)
        {
            return ToBool(reader, column, false);
        }
        public static Boolean ToBool(this IDataReader reader, string column, bool defaultValue)
        {
            try
            {
                if (reader[column] != DBNull.Value)
                    return bool.Parse(reader[column].ToString());
                else
                    return defaultValue;
            }
            catch { }
            return defaultValue;
        }
        public static Boolean? ToBool2(this IDataReader reader, string column)
        {
            Boolean? result = null;
            if (reader[column] != DBNull.Value)
                result = Convert.ToBoolean(reader[column]);
            return result;

        }

        public static int ToInt(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return Convert.ToInt32(reader[column]);
            }
            else
                return 0;
        }

        public static Decimal ToDecimal(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return Convert.ToDecimal(reader[column]);
            }
            else
                return 0;
        }
        public static Guid ToGuid(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return new Guid(reader[column].ToString());
            }
            else
                return Guid.Empty;
        }
        public static DateTime ToDateTime(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return Convert.ToDateTime(reader[column]);
            }
            else
                return DateTime.MinValue;
        }

        public static DateTime? ToDateTime2(this IDataReader reader, string column)
        {
            DateTime? result = null;
            if (reader[column] != DBNull.Value)
                result = Convert.ToDateTime(reader[column]);
            return result;

        }
        public static Char ToChar(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
                return Convert.ToChar(reader[column]);
            else
                return ' ';
        }

        //This converts an integer column to the given enum (T)
        public static T ToEnum<T>(this IDataReader reader, string column)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException(typeof(T).ToString() + " is not an Enum");
            }
            return (T)Enum.ToObject(typeof(T), reader.ToInt(column));
        }
    }
}
