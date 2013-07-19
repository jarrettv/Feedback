namespace Aaa.Common
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    //public static class EnumExtensions
    //{
    //    public static string GetDescription(this Enum enumeration)
    //    {
    //        FieldInfo fi = enumeration.GetType().GetField(enumeration.ToString());
    //        var attributes =
    //            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

    //        return (attributes.Length > 0) ? attributes[0].Description : enumeration.ToString();

    //    }
    //}

    public static class Extensions
    {
        public static string GetDescription<T>(this T t)
        {
            FieldInfo fi = t.GetType().GetField(t.ToString());
            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : t.ToString();

        }
    }
}