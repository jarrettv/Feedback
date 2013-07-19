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

    // Code bind to first DateTime? then DateTimeOffset?
    public static class DateBox
    {

        /// <summary>
        /// Renders a date input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Anonymous object that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString DateBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTime?>> expression)
        {
            return DateBoxFor(htmlHelper, expression, null);
        }

        /// <summary>
        /// Renders a date input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Anonymous object that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString DateBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTime?>> expression, object htmlAttributes)
        {
            return DateBoxFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Renders a date input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Dictionary that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString DateBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTime?>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);

            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("The full name can't be null or empty.", "name");
            }

            var val = (DateTime?)metadata.Model;

            //<div class="input-append date" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
            //    <input size="16" type="text" value="12-02-2012" readonly>
            //    <span class="add-on"><i class="icon-th"></i></span>
            //</div>
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("input-append");
            div.AddCssClass("date");

            if (val.HasValue)
            {
                div.MergeAttribute("data-date", val.Value.ToString("MM/dd/yyyy"));
            }
            
            TagBuilder input = new TagBuilder("input");
            input.MergeAttributes(htmlAttributes);
            input.MergeAttribute("type", "text");
            input.MergeAttribute("size", "10");
            input.MergeAttribute("name", fullName, true);
            input.MergeAttribute("title", metadata.Watermark ?? "mm/dd/yyyy");
            input.GenerateId(fullName);

            string valueParameter = val.HasValue ? val.Value.ToFormatString() : string.Empty;
            string attemptedValue = null;

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    input.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                    attemptedValue = modelState.Value.AttemptedValue;
                }
            }

            input.MergeAttribute("value", attemptedValue ?? valueParameter, true);
            input.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            TagBuilder span = new TagBuilder("span");

            //<span class="add-on"><i class="icon-th"></i></span>


            return MvcHtmlString.Create(input.ToString(TagRenderMode.SelfClosing));
        }


        /// <summary>
        /// Renders a date input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Anonymous object that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString DateBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTimeOffset?>> expression)
        {
            return DateBoxFor(htmlHelper, expression, null);
        }

        /// <summary>
        /// Renders a date input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Anonymous object that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString DateBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTimeOffset?>> expression, object htmlAttributes)
        {
            return DateBoxFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Renders a date input
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a DateTime</param>
        /// <param name="htmlAttributes">Dictionary that contains html attributes</param>
        /// <returns>string representing date input</returns>
        public static MvcHtmlString DateBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
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
            tagBuilder.MergeAttribute("type", "text");
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.MergeAttribute("title", metadata.Watermark ?? "mm/dd/yyyy");
            tagBuilder.GenerateId(fullName);
            tagBuilder.AddCssClass("datepicker");

            // set values
            var val = (DateTimeOffset?)metadata.Model;
            if (val.HasValue)
            {
                tagBuilder.MergeAttribute("data-value", val.Value.ToString("yyyy-MM-dd"));
            }

            string valueParameter = val.HasValue ? val.Value.ToFormatString() : string.Empty;
            string attemptedValue = null;

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                    attemptedValue = modelState.Value.AttemptedValue;
                }
            }

            tagBuilder.MergeAttribute("value", attemptedValue ?? valueParameter, true);
            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
