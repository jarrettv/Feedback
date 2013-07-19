using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Cts.Chronos.Web
{

    /// <summary>
    /// All html helper textbox input extension methods
    /// </summary>
    public static class HelperTextBox
    {
        /// <summary>
        /// Bootstraps the text box for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="inputSize">Size of the input.</param>
        /// <param name="disabled">if set to <c>true</c> [disabled].</param>
        /// <param name="helptext">The helptext.</param>
        /// <returns>An MvcHtmlString</returns>
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, InputSize inputSize = InputSize.Medium, bool disabled = false, bool @readonly = false, bool showVal = false, string helptext = "")
        {
            TagBuilder container = BootstrapCommon.GetRootContainer();
            TagBuilder icontainer = BootstrapCommon.GetInputContainer();
            List<string> css = new List<string>();
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            bool error = BootstrapCommon.HasValidationError(htmlHelper, System.Web.Mvc.ExpressionHelper.GetExpressionText(expression));

            if (error)
            {
                container.AddCssClass("error");
            }

            css.Add(BootstrapCommon.GetCssClass(inputSize));

            if (disabled)
            {
                css.Add("disabled");
                htmlAttributes.Add("disabled", "disabled");
            }

            if (@readonly)
            {
                css.Add("readonly");
                htmlAttributes.Add("readonly", "readonly");
            }

            htmlAttributes.Add("class", BootstrapCommon.GetCss(css));

            MvcHtmlString label = htmlHelper.LabelFor(expression, new { @class = "control-label" });
            //MvcHtmlString label = BootstrapCommon.GetLabel(metadata.PropertyName, metadata.DisplayName);
            MvcHtmlString input = htmlHelper.TextBoxFor(expression, htmlAttributes);

            icontainer.InnerHtml = input.ToString();
            if (showVal) icontainer.InnerHtml += BootstrapCommon.GetValidationMessageSpan(htmlHelper, 
                System.Web.Mvc.ExpressionHelper.GetExpressionText(expression));
            icontainer.InnerHtml += BootstrapCommon.GetHelpSpan(helptext);

            container.InnerHtml = label.ToString() + icontainer.ToString();

            return new MvcHtmlString(container.ToString());
        }
    }
}