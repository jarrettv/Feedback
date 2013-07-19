// <copyright file="HtmlHelpers.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2011 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>3/13/2011 12:25:01 AM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public static class HtmlHelpers
    {   
        /// <summary>
        /// Renders a sort-column link
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="actionName">Action / controller method</param>
        /// <param name="criteria">Filter criteria</param>
        /// <param name="columnName">Column name for sorting</param>
        /// <returns>URL string representing the sort link</returns>
        public static MvcHtmlString SortLink(this HtmlHelper helper, string actionName,
            Criteria criteria, string columnName, string displayName)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("rel", "sort");
            if (columnName == criteria.OrderColumn) tag.MergeAttribute("class", criteria.Desc ? "desc" : "asc");
            tag.MergeAttribute("href", url.Action(actionName) + "?" + criteria.ToStringOrderedBy(columnName));
            tag.MergeAttribute("data-orderColumn", columnName);
            tag.MergeAttribute("data-orderDescending", criteria.Desc ? "False" : "True");
            tag.SetInnerText(displayName);
            return new MvcHtmlString(tag.ToString());
        }

        /// <summary>
        /// Renders a sort-column link
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="actionName">Action / controller method</param>
        /// <param name="criteria">Filter criteria</param>
        /// <param name="columnName">Column name for sorting</param>
        /// <returns>URL string representing the sort link</returns>
        public static MvcHtmlString SortLink(this HtmlHelper helper, string actionName,
            Criteria criteria, string displayName)
        {
            // TODO: lowercase first letter of orderColumn?
            string columnName = displayName.Replace(" ", string.Empty);

            return SortLink(helper, actionName, criteria, columnName, displayName);
        }

        public static MvcHtmlString DateTimeAgoAbbreviation(this HtmlHelper helper, DateTimeOffset date)
        {
            //TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(Settings.Default.TimeZoneInfoId);
            // TODO: use users configured time zone
            DateTimeOffset app = date.ToUniversalTime().Add(TimeZoneInfo.Local.BaseUtcOffset);
            return MvcHtmlString.Create(string.Format("<abbr class=\"timeago\" title=\"{0}Z\">{1}</abbr>",
                date.ToUniversalTime().ToString("s"),
                date.ToFormatString()));//,
            //Settings.Default.TimeZoneDisplay);
        }

        public static MvcHtmlString DateTimeAgoAbbreviationWhen(this HtmlHelper helper, DateTimeOffset date, Func<DateTimeOffset, bool> when)
        {
            return when(date) ? helper.DateTimeAgoAbbreviation(date) : MvcHtmlString.Create(date.ToFormatString());
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<KeyValuePair<string, string>> items, IEnumerable<string> selectedValues)
        {
            var checkBoxListItems = from i in items
                                    select new SelectListItem
                                    {
                                        Text = i.Key,
                                        Value = i.Value,
                                        Selected = selectedValues != null && selectedValues.Contains(i.Value)
                                    };

            return CheckBoxList(helper, name, checkBoxListItems);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> checkBoxListItems)
        {
            return CheckBoxList(helper, name, checkBoxListItems, null);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> checkBoxListItems, object htmlAttributes)
        {
            return CheckBoxList(helper, name, checkBoxListItems, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> checkBoxListItems, IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes == null) htmlAttributes = new Dictionary<string, object>();

            // if no items are selected, try and select from model state
            if (checkBoxListItems.Count() > 0 && !checkBoxListItems.Any(x => x.Selected))
            {
                ModelState modelState;
                if (helper.ViewData.ModelState.TryGetValue(name, out modelState))
                {
                    if (modelState.Value != null)
                    {
                        var selected = checkBoxListItems.FirstOrDefault(x => x.Value == modelState.Value.AttemptedValue);
                        if (selected != null) selected.Selected = true;
                    }
                }
            }

            string html = string.Empty;

            foreach (var listItem in checkBoxListItems)
            {
                var label = new TagBuilder("label");

                // Set the attributes
                var checkbox = new TagBuilder("input");
                checkbox.MergeAttributes(htmlAttributes);
                checkbox.MergeAttribute("type", "checkbox");
                checkbox.MergeAttribute("name", name);
                checkbox.MergeAttribute("value", listItem.Value);

                if (listItem.Selected)
                {
                    checkbox.MergeAttribute("checked", "checked");
                    label.AddCssClass("checked");
                }

                if (htmlAttributes.ContainsKey("disabled")) checkbox.MergeAttribute("disabled", "disabled");

                // Add validation higlighting if necessary
                ModelState state;
                if (helper.ViewData.ModelState.TryGetValue(name, out state) && state.Errors.Count > 0)
                {
                    checkbox.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }

                label.InnerHtml = checkbox.ToString(TagRenderMode.SelfClosing) + listItem.Text;
                html += label.ToString();

                // if item is disabled and item is selected, add hidden input
                if (htmlAttributes.ContainsKey("disabled") && listItem.Selected)
                {
                    var hidden = new TagBuilder("input");
                    hidden.MergeAttribute("type", "hidden");
                    hidden.MergeAttribute("name", name);
                    hidden.MergeAttribute("value", listItem.Value);
                    html += hidden.ToString();
                }
            }
            if (checkBoxListItems.Count() == 0)
            {
                var span = new TagBuilder("span");
                span.AddCssClass("none");
                span.InnerHtml = "(none)";
                html = span.ToString();
            }

            return MvcHtmlString.Create(html);
        }


        public static MvcHtmlString RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> radioItems)
        {
            return RadioButtonList(helper, name, radioItems, null);
        }

        public static MvcHtmlString RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> radioItems, object htmlAttributes)
        {
            return RadioButtonList(helper, name, radioItems, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> radioItems, IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes == null) htmlAttributes = new Dictionary<string, object>();

            // if no items are selected, try and select from model state
            if (radioItems.Count() > 0 && !radioItems.Any(x => x.Selected))
            {
                ModelState modelState;
                if (helper.ViewData.ModelState.TryGetValue(name, out modelState))
                {
                    var selected = radioItems.FirstOrDefault(x => x.Value == modelState.Value.AttemptedValue);
                    if (selected != null) selected.Selected = true;
                }
            }

            string html = string.Empty;

            foreach (var listItem in radioItems)
            {
                var label = new TagBuilder("label");

                // class for individual label styling
                label.AddCssClass(listItem.Text
                    .RemoveSpecialCharacters()
                    .Replace(" ", string.Empty)
                    .ToLower());
                label.AddCssClass("label");

                // set the attributes
                var radio = new TagBuilder("input");
                radio.MergeAttributes(htmlAttributes);
                radio.MergeAttribute("type", "radio");
                radio.MergeAttribute("name", name);
                radio.MergeAttribute("value", listItem.Value);

                if (listItem.Selected)
                {
                    radio.MergeAttribute("checked", "checked");
                    label.AddCssClass("checked");
                }

                if (htmlAttributes.ContainsKey("disabled")) radio.MergeAttribute("disabled", "disabled");

                // Add validation highlighting if necessary
                ModelState state;
                if (helper.ViewData.ModelState.TryGetValue(name, out state) && state.Errors.Count > 0)
                {
                    label.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }

                label.InnerHtml = radio.ToString(TagRenderMode.SelfClosing) + listItem.Text;
                html += label.ToString();

                // if list is disabled and item is selected, add hidden input
                if (htmlAttributes.ContainsKey("disabled") && listItem.Selected)
                {
                    var hidden = new TagBuilder("input");
                    hidden.MergeAttribute("type", "hidden");
                    hidden.MergeAttribute("name", name);
                    hidden.MergeAttribute("value", listItem.Value);
                    html += hidden.ToString();
                }
            }

            if (radioItems.Count() == 0)
            {
                var span = new TagBuilder("span");
                span.AddCssClass("none");
                span.InnerHtml = "(none)";
                html = span.ToString();
            }

            return MvcHtmlString.Create(html);
        }

        public static IEnumerable<SelectListItem> SelItems(this WebViewPage page, string name)
        {
            return page.ViewData[name] as IEnumerable<SelectListItem>;
        }

        public static SelectList SelList(this WebViewPage page, string name)
        {
            return page.ViewData[name] as SelectList;
        }

        public static IEnumerable<SelectListItem> SelItems<T>(this WebViewPage<T> page, string name)
        {
            return page.ViewData[name] as IEnumerable<SelectListItem>;
        }

        public static SelectList SelList<T>(this WebViewPage<T> page, string name)
        {
            return page.ViewData[name] as SelectList;
        }
    }
}