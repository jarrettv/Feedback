using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Reflection;

namespace Cts.Chronos.Web
{
    public class ControlGroup : IDisposable
    {
        private readonly HtmlHelper html;

        public ControlGroup(HtmlHelper html){
            this.html = html;
        }

        public void Dispose(){
            this.html.ViewContext.Writer.Write(this.html.EndControlGroup());
        }
    }

    public static class ControlGroupExtensions
    {
        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,Expression<Func<T, object>> modelProperty){
            return BeginControlGroupFor(html, modelProperty, null);
        }

        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,Expression<Func<T, object>> modelProperty,object htmlAttributes){
            return BeginControlGroupFor(html, modelProperty,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,Expression<Func<T, object>> modelProperty,IDictionary<string, object> htmlAttributes){
            var propertyName = ExpressionHelper.GetPropertyName(modelProperty);
            return BeginControlGroupFor(html, propertyName, htmlAttributes);
        }

        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,string propertyName){
            return BeginControlGroupFor(html, propertyName, null);
        }

        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,string propertyName, object htmlAttributes){
            return BeginControlGroupFor(html, propertyName,HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html,string propertyName,IDictionary<string, object> htmlAttributes){
            var controlGroupWrapper = new TagBuilder("div");
            controlGroupWrapper.MergeAttributes(htmlAttributes);
            controlGroupWrapper.AddCssClass("control-group");
            string partialFieldName = propertyName;
            string fullHtmlFieldName =
                html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(partialFieldName);
            if (!html.ViewData.ModelState.IsValidField(fullHtmlFieldName)){
                controlGroupWrapper.AddCssClass("error");
            }
            string openingTag = controlGroupWrapper.ToString(TagRenderMode.StartTag);
            return MvcHtmlString.Create(openingTag);
        }

        public static IHtmlString EndControlGroup(this HtmlHelper html){
            return MvcHtmlString.Create("</div>");
        }

        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html,Expression<Func<T, object>> modelProperty){
            return ControlGroupFor(html, modelProperty, null);
        }

        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html,Expression<Func<T, object>> modelProperty,object htmlAttributes){
            var propertyName = ExpressionHelper.GetPropertyName(modelProperty);
            return ControlGroupFor(html, propertyName,HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName){
            return ControlGroupFor(html, propertyName, null);
        }

        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName,object htmlAttributes){
            return ControlGroupFor(html, propertyName,HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static ControlGroup ControlGroupFor<T>(this HtmlHelper<T> html, string propertyName,IDictionary<string, object> htmlAttributes){
            html.ViewContext.Writer.Write(BeginControlGroupFor(html, propertyName, htmlAttributes));
            return new ControlGroup(html);
        }

    }

    public static class Alerts
    {
        public const string SUCCESS = "success";
        public const string ATTENTION = "attention";
        public const string ERROR = "error";
        public const string INFORMATION = "info";

        public static string[] ALL{
            get { return new[] {SUCCESS, ATTENTION, INFORMATION, ERROR}; }
        }
    }

    /// <summary>
    /// The <code>ExpressionHelper</code> class provides helper methods to get the model name from an expression.
    /// </summary>
    public class ExpressionHelper
    {
        /// <summary>
        /// Gets the model name from a lambda expression.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string GetPropertyName<TModel>(Expression<Func<TModel, object>> expression)
        {
            var expr = (LambdaExpression)expression;
            if (expr.Body.NodeType == ExpressionType.Convert)
            {
                var ue = expression.Body as UnaryExpression;
                return String.Join(".", GetProperties(ue.Operand).Select(p => p.Name));
            }
            return System.Web.Mvc.ExpressionHelper.GetExpressionText(expr);
        }

        /// <summary>
        /// Return a list of properties for an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list of properties.</returns>
        private static IEnumerable<PropertyInfo> GetProperties(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null) yield break;

            var property = memberExpression.Member as PropertyInfo;
            foreach (var propertyInfo in GetProperties(memberExpression.Expression))
            {
                yield return propertyInfo;
            }
            yield return property;
        }
    }
}
