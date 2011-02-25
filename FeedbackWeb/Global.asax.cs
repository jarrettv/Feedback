using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity.Database;
using System.Diagnostics;
using System.Reflection;

namespace FeedbackWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RenderTimeAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Feedback", "feedback/{id}", new { controller = "Home", action = "Feedback" });
            routes.MapRoute("Results", "results/{secretKey}", new { controller = "Home", action = "Results" });
            routes.MapRoute("Thanks", "thanks/{id}", new { controller = "Home", action = "Thanks" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            DbDatabase.SetInitializer<Catalog>(new CatalogSeed());

            //// register controllers
            //var builder = new ContainerBuilder();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.Register(x => new Catalog());

            //// Autofac implementation of IDependencyResolver
            //IContainer container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }

    public class RenderTimeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items["startTime"] = DateTimeOffset.Now;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var start = (DateTimeOffset)filterContext.HttpContext.Items["startTime"];
            var time = TimeSpan.FromTicks(DateTimeOffset.Now.Ticks - start.Ticks);
            Trace.WriteLine(string.Format("Render time was: {0}s", time.TotalSeconds));
        }
    }
}