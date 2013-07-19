// <copyright file="SelectListHelper.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2012 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>8/9/2012 4:33:30 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    //using System.Web.WebPages.Html;

    public static class SelectListHelper
    {
        public static readonly SelectListItem EmptyItem = 
                new SelectListItem()
                {
                    Text = string.Empty,
                    Value = string.Empty,
                };

        public static readonly IEnumerable<SelectListItem> Empty = new[] { EmptyItem };

        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> q, string selectedCode,
            bool includeBlank = true, Func<T, string> display = null) where T : CodeName
        {
            if (display == null) display = (x) => x.Name;
            var list = q
                .Where(x => !x.Inactive || x.Code == selectedCode)
                .Select(x => new SelectListItem()
                {
                    Text = display(x),
                    Value = x.Code,
                    Selected = x.Code == selectedCode,
                }).OrderBy(x => x.Text);
            return includeBlank ? list.Concat(new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = String.Empty,
                    Value = String.Empty,
                    Selected = list.Any(x => !x.Selected),
                }
            }).OrderBy(x => x.Text) : list;
        }

        public static IEnumerable<SelectListItem> ToSelectList<T>(this Enum e, T selected) where T : struct
        {
            return ToSelectList<T>(e, selected, true);
        }

        public static IEnumerable<SelectListItem> ToSelectList<T>(this Enum e, T selected, bool includeBlank) where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                .Select(x => new SelectListItem()
                {
                    Text = (includeBlank && Convert.ToInt32(x) == default(int)) ? string.Empty : x.ToString().SplitPascalCase(),
                    Value = (includeBlank && Convert.ToInt32(x) == default(int)) ? (default(int)).ToString() : Convert.ToInt32(x).ToString(),
                    Selected = x.Equals(selected),
                })
                .OrderBy(x => x.Text).AsEnumerable();
        }

        public static IEnumerable<SelectListItem> SelectedInSelectListOf<T>(this T selected) where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                .Select(x => new SelectListItem()
                {
                    Text = x.ToString(),
                    Value = Convert.ToInt32(x).ToString(),
                    Selected = x.Equals(selected),
                })
                .OrderBy(x => x.Text);
        }
    }
}
