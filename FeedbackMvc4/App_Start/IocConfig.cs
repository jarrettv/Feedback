using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FeedbackMvc4
{
    public class IocConfig
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<FeedbackDb>()
                .InstancePerLifetimeScope();
        }
    }
}