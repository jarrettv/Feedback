// <copyright file="SettingParser.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2012 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jaden</author>
// <date>2/15/2012 10:21:54 AM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Static class to parse delimited setting strings into lists or dictionaries
    /// </summary>
    public static class SettingParser<K, T>
    {
        public static List<T[]> ParseSetting(string setting, char primaryDelimiter, char secondaryDelimiter)
        {
            return setting
                .Split(primaryDelimiter)
                .Select(x => x.Split(secondaryDelimiter)
                    .Select(y => (T)Convert.ChangeType(y, typeof(T)))
                    .ToArray())
                .ToList();
        }

        public static Dictionary<K, T> StoreSetting(string setting, char primaryDelimiter, char secondaryDelimiter)
        {
            var result = new Dictionary<K, T>();

            setting
                .Split(primaryDelimiter)
                .Select(x => x.Split(secondaryDelimiter))
                .ToList()
                .ForEach(x => result.Add((K)Convert.ChangeType(x[0], typeof(K)), (T)Convert.ChangeType(x[1], typeof(T))));

            return result;
        }
    }
}
