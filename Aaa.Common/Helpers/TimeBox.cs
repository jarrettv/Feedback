// <copyright file="DateBox.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2011 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>10/15/2011 10:30:39 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using HtmlHelper = System.Web.Mvc.HtmlHelper;
    using ModelState = System.Web.Mvc.ModelState;

    public static class TimeBox
    {
        /// <summary>
        /// Renders a time input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Anonymous object that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString TimeBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTimeOffset?>> expression)
        {
            return TimeBoxFor(htmlHelper, expression, null);
        }

        /// <summary>
        /// Renders a time input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Anonymous object that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString TimeBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTimeOffset?>> expression, object htmlAttributes)
        {
            return TimeBoxFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Renders a time input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Dictionary that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString TimeBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTimeOffset?>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);

            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("The full name can't be null or empty.", "name");
            }

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", "time");
            tagBuilder.MergeAttribute("name", fullName + "Time", true);
            tagBuilder.MergeAttribute("title", metadata.Watermark ?? "hh:mm tt");
            tagBuilder.GenerateId(fullName + "Time");

            // set values
            var val = (DateTimeOffset?)metadata.Model;
            if (val.HasValue)
            {
                tagBuilder.MergeAttribute("data-value", val.Value.DateTime.ToShortTimeString().Replace("12:00 AM", string.Empty));
            }

            string valueParameter = val.HasValue ? val.Value.DateTime.ToShortTimeString().Replace("12:00 AM", string.Empty) : string.Empty;
            string attemptedValue = null;

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName + "Time", out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                    if (modelState.Value != null) attemptedValue = modelState.Value.AttemptedValue;
                }
            }
            else if (!string.IsNullOrWhiteSpace(htmlHelper.ViewContext.HttpContext.Request.Form[fullName + "Time"]))
            {
                attemptedValue = htmlHelper.ViewContext.HttpContext.Request.Form[fullName + "Time"];
            }

            tagBuilder.MergeAttribute("value", attemptedValue ?? valueParameter, true);
            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
