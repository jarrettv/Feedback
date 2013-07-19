using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aaa.Common
{
    public class Menu
    {
        public Menu()
        {
            this.Items = new List<MenuItem>();
        }

        public IList<MenuItem> Items { get; protected set; }
    }

    public static class MenuTable
    {
        private static IDictionary<string, Menu> menus = new Dictionary<string, Menu>();

        public static IDictionary<string, Menu> Menus { get { return menus; } }
    }

    public class MenuItem
    {
        public MenuItem()
        {
            this.IsVisible = this.DefaultIsVisible;
            this.GetHref = (url) => url.RouteUrl(this.RouteName);
            this.Roles = new string[0];
            this.Children = new List<MenuItem>();
        }

        public MenuItem(string text)
            : this()
        {
            this.Text = text;
            this.RouteName = text;
        }

        public MenuItem(string text, string role) : this(text, text, role) { }

        public MenuItem(string text, string routeName, string role)
            : this()
        {
            this.Text = text;
            this.RouteName = routeName;
            this.Roles = new[] { role };
        }

        public string Icon { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string RouteName { get; set; }
        public string[] Roles { get; set; }
        public List<MenuItem> Children { get; set; }
        public Func<UrlHelper, string> GetHref { get; set; }
        public Func<ViewContext, bool> IsVisible { get; set; }

        public override string ToString()
        {
            return this.Text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return this.Text == ((MenuItem)obj).Text;
        }

        public override int GetHashCode()
        {
            return this.Text.GetHashCode();
        }

        public bool HasChildren()
        {
            return this.Children != null && this.Children.Count > 0;
        }

        public bool IsSelected(ViewContext ctx)
        {
            var url = new UrlHelper(ctx.RequestContext);

            // see if any of the children are selected
            if (this.HasChildren())
            {
                if (this.Children.Any(x => x.IsSelected(ctx)))
                    return true;
            }

            // see if exact match to current url
            var href = this.GetHref(url);
            return href.Substring(href.IndexOf('/')) == ctx.HttpContext.Request.Url.PathAndQuery;
        }

        protected bool DefaultIsVisible(ViewContext ctx)
        {
            // when there are children, visible when any child item is visible
            if (this.HasChildren())
            {
                return this.Children.Any(x => x.IsVisible(ctx));
            }

            // otherwise, no roles then show or check and see if in a defined role
            return this.Roles.Length == 0 ||
                this.Roles.Any(x => ctx.HttpContext.User.IsInRole(x));
        }
    }
}
