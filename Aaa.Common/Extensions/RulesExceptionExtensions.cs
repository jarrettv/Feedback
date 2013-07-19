using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Aaa.Common
{
    public static class RulesExceptionExtensions
    {
        public static void AddModelStateErrors(this RulesException ex, Action<string, string> addToModelState, string prefix)
        {
            AddModelStateErrors(ex, addToModelState, prefix, x => true);
        }

        public static void AddModelStateErrors(this RulesException ex, Action<string, string> addToModelState, string prefix, Func<ErrorInfo, bool> errorFilter)
        {
            if (errorFilter == null) throw new ArgumentNullException("errorFilter");
            prefix = prefix == null ? "" : prefix + ".";
            foreach (var errorInfo in ex.Errors.Where(errorFilter))
            {
                var key = prefix + errorInfo.PropertyName;
                addToModelState(key, errorInfo.ErrorMessage);
            }
        }
    }
}
