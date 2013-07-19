using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Cts.Chronos.Web
{
    public static class Helpers
    {
        /// <summary>
        /// Converts a list of values to a data-source list for usage with type-ahead
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ToSource(this IEnumerable<string> values)
        {
            return "[" + string.Join(",", values.Select(x => "\"" + x.Trim() + "\"").ToArray()) + "]";
        }
    }
}