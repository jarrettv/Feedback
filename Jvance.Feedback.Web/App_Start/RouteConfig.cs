using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JVance.Feedback.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Presentations", "presentations", new { controller = "Presentation", action = "Index" });
            routes.MapRoute("Historic", "presentations/old", new { controller = "Presentation", action = "Historic" });
            
            routes.MapRoute("CodeStock", "codestock", new { controller = "Home", action = "CodeStock" });
            routes.MapRoute("About", "about", new { controller = "Home", action = "About" });

            routes.MapRoute("Admin", "admin", new { controller = "Admin", action = "Index" });
            routes.MapRoute("Templates", "admin/templates", new { controller = "Admin", action = "Templates" });
            routes.MapRoute("Ratings", "admin/ratings", new { controller = "Admin", action = "Ratings" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
