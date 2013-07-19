// <copyright file="Container.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2011 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/12/2011 4:35:06 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System.Diagnostics;
    using System.Web;
    using Autofac;
    using Autofac.Integration.Mvc;

    [DebuggerStepThrough]
    public static class Container
    {
        public static ILifetimeScope Get()
        {            
            // look in HttpContext
            //if (HttpContext.Current != null && HttpContext.Current.Items.Contains(typeof(ILifetimeScope)))
            //    return ((ILifetimeScope)HttpContext.Current.Items[typeof(ILifetimeScope)]);

            if (AutofacDependencyResolver.Current != null)
                return AutofacDependencyResolver.Current.RequestLifetimeScope;

            return null;
        }
    }
}
