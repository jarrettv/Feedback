// <copyright file="TypeAheadBox.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2012 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jzeiger</author>
// <date>6/15/2012 2:38:39 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    public static class TypeAheadBox
    {

        /// <summary>
        /// Renders a textbox input for typeahead
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a string</param>
        /// <param name="htmlAttributes">Anonymous object that contains html attributes</param>
        /// <returns>string representing typeahead input</returns>
        public static MvcHtmlString TypeAheadBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, string>> expression)
        {
            return TypeAheadBoxFor(htmlHelper, expression, null);
        }

        /// <summary>
        /// Renders a textbox input for typeahead
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a string</param>
        /// <param name="htmlAttributes">Anonymous object that contains html attributes</param>
        /// <returns>string representing typeahead input</returns>
        public static MvcHtmlString TypeAheadBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, string>> expression, object htmlAttributes)
        {
            return TypeAheadBoxFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Renders a textbox input for typeahead
        /// </summary>
        /// <param name="helper">This HTML helper</param>
        /// <param name="expression">Expression that returns a string</param>
        /// <param name="htmlAttributes">Dictionary that contains html attributes</param>
        /// <returns>string representing typeahead input</returns>
        public static MvcHtmlString TypeAheadBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, string>> expression, IDictionary<string, object> htmlAttributes)
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
            tagBuilder.MergeAttribute("class", "typeahead");
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.GenerateId(fullName);

            // set values
            var val = (string)metadata.Model;
            string valueParameter = !string.IsNullOrWhiteSpace(val) ? val : string.Empty;
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
